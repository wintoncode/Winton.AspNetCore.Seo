using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Winton.AspNetCore.Seo.Extensions;

namespace Winton.AspNetCore.Seo
{
    internal class Startup : IStartup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc(builder => builder.MapSeoRoutes());
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddSeo<SitemapConfig>();
            return services.BuildServiceProvider();
        }
    }
}