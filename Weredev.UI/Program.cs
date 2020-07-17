using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.IO;
using SumoLogic.Logging.AspNetCore;

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
                .ConfigureLogging((context, logging) =>
                {
                    if (context.HostingEnvironment.IsDevelopment())
                    {
                        // logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Information);
                        logging.AddConsole();
                    }
                    else
                    {  
                        var sumoLogicOptions = context.Configuration.GetSection("Logging:SumoLogic").Get<LoggerOptions>();
                        logging.AddSumoLogic(sumoLogicOptions);
                    }
                })

                // .UseIISIntegration()
                .UseStartup<Startup>();
    }
}
