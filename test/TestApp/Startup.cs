using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
                .UseEndpoints(endpoints => { endpoints.MapDefaultControllerRoute(); });
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddSeo(_configuration)
                .AddControllersWithViews();
        }
    }
}