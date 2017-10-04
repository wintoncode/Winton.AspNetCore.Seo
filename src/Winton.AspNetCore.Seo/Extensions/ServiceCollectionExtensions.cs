// Copyright (c) Winton. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Winton.AspNetCore.Seo.HeaderMetadata;
using Winton.AspNetCore.Seo.Robots;
using Winton.AspNetCore.Seo.Sitemaps;

namespace Winton.AspNetCore.Seo.Extensions
{
    /// <summary>
    ///     Extension methods for setting up the required service in an <see cref="IServiceCollection" />.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        ///     Adds the implementation of <see cref="ISitemapConfig" /> to the specified <see cref="IServiceCollection" /> and
        ///     configures all other required services using defaults if they have not already been added to the
        ///     <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> to add the services to.</param>
        /// <param name="sitemapConfig">The <see cref="ISitemapConfig" /> to use.</param>
        /// <returns>The <see cref="IServiceCollection" /> so that additional calls can be chained.</returns>
        public static IServiceCollection AddSeo(this IServiceCollection services, ISitemapConfig sitemapConfig)
        {
            services.TryAddSingleton(sitemapConfig);
            return services.AddSeo();
        }

        /// <summary>
        ///     Adds the type <typeparamref name="TSitemapConfig" /> to the specified <see cref="IServiceCollection" /> as the
        ///     implementation of <see cref="ISitemapConfig" /> and configures all other required services using defaults if
        ///     they have not already been added to the <see cref="IServiceCollection" />.
        /// </summary>
        /// <typeparam name="TSitemapConfig">The type to register as the implementation of <see cref="ISitemapConfig" />.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection" /> to add the services to.</param>
        /// <returns>The <see cref="IServiceCollection" /> so that additional calls can be chained.</returns>
        public static IServiceCollection AddSeo<TSitemapConfig>(this IServiceCollection services)
            where TSitemapConfig : class, ISitemapConfig
        {
            services.TryAddSingleton<ISitemapConfig, TSitemapConfig>();
            return services.AddSeo();
        }

        private static IServiceCollection AddSeo(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

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