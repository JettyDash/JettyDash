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
                    webBuilder.UseStartup<Startup>()
                        .UseUrls("http://*:5000");
                    // .ConfigureKestrel(opts =>
                    // {
                    // opts.ListenAnyIP(5000); // listen on http://*:5003
                    // });

                });

    }
}