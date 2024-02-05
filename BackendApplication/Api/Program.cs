using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    ConfigureKestrel(webBuilder);
                    webBuilder.UseStartup<Startup>();
                });

        private static void ConfigureKestrel(IWebHostBuilder webBuilder)
        {
            webBuilder.UseKestrel((context, options) =>
            {
                var endpoints = context.Configuration.GetSection("Kestrel:Endpoints");
                var certPath = endpoints.GetValue<string>("Https:Certificate:Path");
                var keyPath = endpoints.GetValue<string>("Https:Certificate:Password");
                var certificate = new X509Certificate2(certPath, keyPath);

                options.Listen(IPAddress.Loopback, 5268); // HTTP
                options.Listen(IPAddress.Loopback, 7268, listenOptions =>
                {
                    listenOptions.UseHttps(certificate); // HTTPS
                });
            });
        }
    }
}