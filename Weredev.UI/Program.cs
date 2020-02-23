using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Weredev.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.SetBasePath(Directory.GetCurrentDirectory());

                    if (hostingContext.HostingEnvironment.EnvironmentName == "Development")
                        config.AddUserSecrets<Startup>();
                })
                .UseKestrel(serverOptions =>
                {
                    // Set properties and call methods on options
                })

                // .UseIISIntegration()
                .UseStartup<Startup>();
    }
}
