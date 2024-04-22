using System.Text;
using Api.Middleware;
using AutoMapper;
using Business.Mapper;
using Business.Preprocessor;
using Business.Preprocessor.Common;
using Business.Service;
using Business.Validator;
using FluentValidation;
using FluentValidation.AspNetCore;
using HealthChecks.UI.Client;
using Infrastructure.DbContext;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Schemes.Config.Token;
using Schemes.Dto;

namespace Api;

public class Startup
{
    public readonly IConfiguration Configuration;

    public Startup(IConfiguration configuration)
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddConfiguration(configuration);

        builder.AddEnvironmentVariables();

        Configuration = builder.Build();
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<BackendDbContext>(options =>
        {
            // options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddSingleton<IDapperServiceFactory, DapperServiceFactory>();


        services.AddHttpContextAccessor();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUserService, UserService>();

        // MediatR
        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(assembly);
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(CreateHostConnectionPipelineInitializer<,>));
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(CreateHostConnectionValidationBehaviour<,>));
                cfg.AddOpenBehavior(typeof(DbContextTransactionBehaviour<,>)); // Atomic transaction for each requestsh
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

        // JWT
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


        services.AddHealthChecks()
            .AddNpgSql(Configuration.GetConnectionString("DefaultConnection"), name: "PostgreDbHealthCheck");
            // .AddSqlServer(Configuration.GetConnectionString("DefaultConnection"), name: "SqlServerDbHealthCheck");

        services.AddControllers();

        // FluentValidation
        services.AddFluentValidationAutoValidation();
        // TODO: Open this when fe is ready
        // ValidatorOptions.Global.Defaul   tRuleLevelCascadeMode = CascadeMode.Stop;

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
        app.UseHsts();
        app.UseHttpsRedirection();
        
        app.UseHealthChecks("/health", new HealthCheckOptions
        {
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });

        app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
        

        app.UseCors(options =>
            options.WithOrigins("*").AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        app.UseAuthentication();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapGet("/", async context =>
            {
                await context.Response.WriteAsync($"App is running on {env.EnvironmentName} {DateTime.UtcNow}!");
                
            });
            endpoints.MapHealthChecks("/health");
            endpoints.MapControllers();
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
        
    }
}