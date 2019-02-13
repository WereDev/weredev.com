using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Weredev.Providers.Flickr;
using Weredev.UI.Domain.Interfaces;

namespace Weredev
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            
            var flickrApiKey = Configuration.GetValue<string>("Flickr.ApiKey");
            var flickrUserId = Configuration.GetValue<string>("Flickr.UserId");
            services.AddSingleton<ITravelImageProvider>(new FlickrProvider(flickrApiKey, flickrUserId));

            //GetCssVersions();

            // In production, the React files will be served from this directory
            // services.AddSpaStaticFiles(configuration =>
            // {
            //     configuration.RootPath = "ClientApp/build";
            // });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            //app.UseSpaStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            

            // app.UseSpa(spa =>
            // {
            //     spa.Options.SourcePath = "ClientApp";

            //     if (env.IsDevelopment())
            //     {
            //         spa.UseReactDevelopmentServer(npmScript: "start");
            //     }
            // });
        }

        public void GetJsVersions() {

        }

        public void GetCssVersions() {
            var currentDir = Directory.GetCurrentDirectory();
            var cssFolder = "wwwroot/static/css";
            var fullPath = Path.Combine(currentDir, cssFolder);

            var files = Directory.GetFiles(fullPath);

            var cssFiles = new List<string>();
            foreach (var file in files) {
                if (file.EndsWith(".css", StringComparison.CurrentCultureIgnoreCase)) {
                    var cssFileName = Path.GetFileName(file);
                    var cssFilePath = Path.Combine("/static/css/", cssFileName);
                }
            }
        }
    }
}
