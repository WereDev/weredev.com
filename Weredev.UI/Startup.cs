using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using Weredev.Providers.Flickr;
using Weredev.Providers.GitHub;
using Weredev.UI.Domain.Interfaces;
using Weredev.UI.Domain.Services;

namespace Weredev.UI
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
            services.AddMemoryCache();
            services.AddControllersWithViews();

            services.Configure<RouteOptions>(options =>
            {
                options.AppendTrailingSlash = true;
                options.LowercaseUrls = true;
            });

            var flickrApiKey = Configuration.GetValue<string>("Flickr.ApiKey");
            var flickrUserId = Configuration.GetValue<string>("Flickr.UserId");
            services.AddSingleton<ICacheProvider, HttpCacheProvider>();
            services.AddSingleton<ITravelImageProvider>(new FlickrProvider(flickrApiKey, flickrUserId));
            services.AddSingleton<IGitHubProvider, GitHubProvider>();
            services.AddSingleton<ITravelService, TravelService>();
            services.AddSingleton<ICodeRepoService, CodeRepoService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.EnvironmentName == "Development")
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    const int durationInSeconds = 60 * 60 * 24;
                    ctx.Context.Response.Headers[HeaderNames.CacheControl] = "public,max-age=" + durationInSeconds;
                },
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
