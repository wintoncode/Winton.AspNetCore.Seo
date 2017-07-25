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
            services.AddMvcCore(options => options.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter()));
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.TryAddTransient<IRobotsTxtFactory, RobotsTxtFactory>();
            services.TryAddSingleton(sitemapConfig);
            services.TryAddSingleton<ISitemapFactory, SitemapFactory>();
            services.AddHeaderMetadata();
            return services;
        }
    }
}