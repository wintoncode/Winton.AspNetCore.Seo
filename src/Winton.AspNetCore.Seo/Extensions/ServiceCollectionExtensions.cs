using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.FileProviders;
using Winton.AspNetCore.Seo.Robots;
using Winton.AspNetCore.Seo.Sitemaps;

namespace Winton.AspNetCore.Seo.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSeo(this IServiceCollection services, ISitemapConfig sitemapConfig)
        {
            services.AddMvcCore(options => options.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter()));
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.TryAddTransient<IRobotsTxtFactory, RobotsTxtFactory>();
            services.TryAddSingleton(sitemapConfig);
            services.TryAddSingleton<ISitemapFactory, SitemapFactory>();
            services.AddOgMetadata();
            return services;
        }

        private static void AddOgMetadata(this IServiceCollection services)
        {
            Assembly assembly = typeof(ServiceCollectionExtensions).GetTypeInfo().Assembly;

            var embeddedFileProvider = new EmbeddedFileProvider(assembly, "Winton.AspNetCore.Seo");

            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.FileProviders.Add(embeddedFileProvider);
            });
        }
    }
}
