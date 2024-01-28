using Business.Services;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Schemes.Vault;
using VaultSharp;
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
        VaultConfig? vaultConfig = Configuration.GetSection("Vault").Get<VaultConfig>();
        
        VaultClientSettings vaultClientSettings = new VaultClientSettings(vaultConfig.Address, new TokenAuthMethodInfo(vaultConfig.Token));
        IVaultClient vaultClient = new VaultClient(vaultClientSettings);
        services.AddSingleton(vaultClient);
        
        services.AddScoped<IVaultService, VaultService>();
        

        // Database yapılandırması        
        services.AddDbContext<BackendDbContext>(options =>
        {
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        });
        

        // // MediatR
        // foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
        // {
        //     services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly));
        // }
        //
        // // AutoMapper
        // var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new MapperConfig()));
        // services.AddSingleton(mapperConfig.CreateMapper());
        //

        
        services.AddEndpointsApiExplorer();
        // services.AddHttpContextAccessor();
        
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "JettyDash Api", Version = "v1.0" });

        });

        services.AddHealthChecks();
        services.AddControllers();
        

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
        // app.UseMiddleware<GlobalExceptionHandlerMiddleware>();


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