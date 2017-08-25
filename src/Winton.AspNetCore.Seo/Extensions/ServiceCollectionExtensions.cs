using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Winton.AspNetCore.Seo.HeaderMetadata;
using Winton.AspNetCore.Seo.Robots;
using Winton.AspNetCore.Seo.Sitemaps;

namespace Winton.AspNetCore.Seo.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSeo(this IServiceCollection services, ISitemapConfig sitemapConfig)
        {
            services.TryAddSingleton(sitemapConfig);
            return services.AddSeo();
        }

        public static IServiceCollection AddSeo<TSitemapConfig>(this IServiceCollection services)
            where TSitemapConfig : class, ISitemapConfig
        {
            services.TryAddSingleton<ISitemapConfig, TSitemapConfig>();
            return services.AddSeo();
        }

        private static IServiceCollection AddSeo(this IServiceCollection services)
        {
            services.AddMvcCore(
                options => options.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter()));
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.TryAddSingleton<IRobotsTxtOptions, DefaultRobotsTxtOptions>();
            services.TryAddTransient<IRobotsTxtFactory, RobotsTxtFactory>();
            services.TryAddSingleton<ISitemapFactory, SitemapFactory>();
            return services.AddHeaderMetadata();
        }
    }
}