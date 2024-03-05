using System.Diagnostics;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
            
            var startTime = Stopwatch.GetTimestamp();

            var host = CreateHostBuilder(args).Build();

            var lifetime = host.Services.GetService<IHostApplicationLifetime>()!;
            
            lifetime.ApplicationStarted.Register(() =>
            {
                var elapsedTime = Stopwatch.GetElapsedTime(startTime).TotalMilliseconds;
                Console.WriteLine($"Startup time: {elapsedTime}ms.");
            });

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                        // .UseUrls("http://*:5000");
                    .ConfigureKestrel(opts =>
                    {
                    opts.ListenAnyIP(5000); // listen on http://*:5003
                    });

                });

    }
}