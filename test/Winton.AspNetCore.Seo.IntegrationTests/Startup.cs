using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Winton.AspNetCore.Seo.Extensions;

namespace Winton.AspNetCore.Seo
{
    public sealed class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc(builder => builder.MapSeoRoutes());
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSeo<SitemapConfig>();
        }
    }
}