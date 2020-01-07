using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Winton.AspNetCore.Seo.Extensions;
using Winton.AspNetCore.Seo.Sitemaps;

namespace Winton.AspNetCore.Seo.TestApp
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app
                .UseDeveloperExceptionPage()
                .UseHttpsRedirection()
                .UseHsts()
                .UseStaticFiles()
                .UseRouting()
                .UseEndpoints(
                    endpoints =>
                    {
                        endpoints.MapDefaultControllerRoute();
                    });
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var sitemapConfig = new SitemapConfig
            {
                Urls = new List<SitemapConfigUrl>
                {
                    new SitemapConfigUrl
                    {
                        Priority = 0.9M,
                        RelativeUrl = "/login"
                    }
                }
            };
            services
                .AddSeo(sitemapConfig)
                .AddControllersWithViews();
        }

        public class SitemapConfig : ISitemapConfig
        {
            public List<SitemapConfigUrl> Urls { get; set; } = new List<SitemapConfigUrl>();
        }
    }
}