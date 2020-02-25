using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using Weredev.Providers.Flickr;
using Weredev.Providers.GitHub;
using Weredev.Domain.Interfaces;
using Weredev.Domain.Services;

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

            services.AddSingleton<ICacheProvider, HttpCacheProvider>();
            var (flickrApiKey, flickrUserId) = GetFlickrConfig();
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

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto,
            });

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

        private (string, string) GetFlickrConfig()
        {
            var flickrSection = Configuration.GetSection("Flickr");
            var flickrApiKey = flickrSection.GetValue<string>("ApiKey");
            var flickrUserId = flickrSection.GetValue<string>("UserId");
            return (flickrApiKey, flickrUserId);
        }
    }
}
