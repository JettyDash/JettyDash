using System.Text;
using Api.Middlewares;
using AutoMapper;
using Business.Cqrs;
using Business.Mapper;
using Business.Preprocessor;
using Business.Services;
using Business.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.DbContext;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Schemes.Dtos;
using Schemes.Token;
using Schemes.Vault;
using VaultSharp;
using VaultSharp.V1.AuthMethods;
using VaultSharp.V1.AuthMethods.Token;

namespace Api;

public class Startup
{
    public IConfiguration Configuration;

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        // Vault
        VaultConfig vaultConfig = Configuration.GetSection("VaultConfig").Get<VaultConfig>();
        services.Configure<VaultConfig>(Configuration.GetSection("VaultConfig"));

        // Vault Authenticate
        IAuthMethodInfo authMethod = new TokenAuthMethodInfo(vaultConfig.Token);
        VaultClientSettings vaultClientSettings = new VaultClientSettings(vaultConfig.Address, authMethod);
        IVaultClient vaultClient = new VaultClient(vaultClientSettings);
        
        services.AddSingleton(vaultClient);
        services.AddScoped<IVaultService, VaultService>();


        // Database yapılandırması        
        services.AddDbContext<BackendDbContext>(options =>
        {
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        });

        // Dapper
        // services.AddScoped<IDapperService, DapperService>();
        services.AddSingleton<IDapperServiceFactory, DapperServiceFactory>();


        // MediatR
        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(assembly);
                cfg.AddBehavior(typeof(IPipelineBehavior<, >),typeof(CreateHostConnectionCommandPreprocessor1<, >));
                cfg.AddBehavior(typeof(IPipelineBehavior<, >),typeof(CreateHostConnectionCommandPreprocessor2<, >));
                
                /* Without using where TRequest : CreateHostConnectionCommand
                cfg.AddBehavior(typeof(IPipelineBehavior<CreateHostConnectionCommand, ApiResponse<ConnectionResponse>>),typeof(CreateHostConnectionCommandPreprocessor3<CreateHostConnectionCommand, ApiResponse<ConnectionResponse>>));
                */


            });
        }

        // AutoMapper
        var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new MapperConfig()));
        services.AddSingleton(mapperConfig.CreateMapper());


        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "JettyDash Api", Version = "v1.0" });

            var securityScheme = new OpenApiSecurityScheme
            {
                Name = "JettyDash Api",
                Description = "Enter JWT Bearer token **_only_**",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };
            c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                { securityScheme, new string[] { } }
            });
        });


        services.AddHttpContextAccessor();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUserService, UserService>();

        JwtConfig jwtConfig = Configuration.GetSection("JwtConfig").Get<JwtConfig>();
        services.Configure<JwtConfig>(Configuration.GetSection("JwtConfig"));

        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = true;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtConfig.Issuer,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Secret)),
                ValidAudience = jwtConfig.Audience,
                ValidateAudience = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromMinutes(2)
            };
        });
        services.AddAuthorization(options =>
        {
            options.AddPolicy(Constants.Roles.Admin, policy => policy.RequireRole(Constants.Roles.Admin));
            options.AddPolicy(Constants.Roles.Personnel, policy => policy.RequireRole(Constants.Roles.Personnel));
            options.AddPolicy(Constants.Roles.Guest, policy => policy.RequireRole(Constants.Roles.Guest));
        });


        services.AddHealthChecks();
        services.AddControllers();

        // FluentValidation
        services.AddFluentValidationAutoValidation();
        // TODO: Open this when fe is ready
        // ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Stop;

        services.AddScoped<IHandlerValidator, HandlerValidator>();
        services.AddScoped<IValidator<CreateUrlConnectionRequest>, CreateUrlConnectionRequestValidator>();
        services.AddScoped<IValidator<CreateHostConnectionRequest>, CreateHostConnectionRequestValidator>();

        services.AddScoped<IValidator<UpdateUrlConnectionRequest>, UpdateUrlConnectionRequestValidator>();
        services.AddScoped<IValidator<UpdateHostConnectionRequest>, UpdateHostConnectionRequestValidator>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHealthChecks("/health");
        app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

        app.UseCors(options =>
            options.WithOrigins("*").AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        app.UseAuthentication();
        app.UseRouting();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }
}