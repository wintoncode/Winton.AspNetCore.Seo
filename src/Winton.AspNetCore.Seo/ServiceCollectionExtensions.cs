// Copyright (c) Winton. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Winton.AspNetCore.Seo.HeaderMetadata.OpenGraph;
using Winton.AspNetCore.Seo.Robots;
using Winton.AspNetCore.Seo.Sitemaps;

namespace Winton.AspNetCore.Seo
{
    /// <summary>
    ///     Extension methods for setting up the required services in an <see cref="IServiceCollection" />.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        ///     Adds the required services for the SEO controller and OpenGraph tag helper.
        ///     Configures the options from the <see cref="IConfiguration" />.
        /// </summary>
        /// <remarks>
        ///     No defaults are used, the configuration must define all the required <see cref="SeoOptions" />.
        ///     It expects the <see cref="IConfiguration" /> to contain a section called <c>"Seo"</c>.
        /// </remarks>
        /// <param name="services">The <see cref="IServiceCollection" /> to register the services with.</param>
        /// <param name="configuration">The configuration to bind the options to.</param>
        /// <returns>The <see cref="IServiceCollection" />.</returns>
        public static IServiceCollection AddSeo(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddSeo(configuration.GetSection("Seo").Bind);
        }

        /// <summary>
        ///     Adds the required services for the SEO controller and OpenGraph tag helper.
        ///     Configures the options using a delegate.
        /// </summary>
        /// <remarks>
        ///     No defaults are used, the delegate must set all of the required <see cref="SeoOptions" />.
        /// </remarks>
        /// <param name="services">The <see cref="IServiceCollection" /> to register the services with.</param>
        /// <param name="configureOptions">A delegate that configures the <see cref="SeoOptions" />.</param>
        /// <returns>The <see cref="IServiceCollection" />.</returns>
        public static IServiceCollection AddSeo(this IServiceCollection services, Action<SeoOptions> configureOptions)
        {
            return services.AddSeo((options, _) => configureOptions(options));
        }

        /// <summary>
        ///     Adds the required services for the SEO controller and OpenGraph tag helper.
        ///     Configures the options using a delegate which has access to the <see cref="IWebHostEnvironment" />.
        /// </summary>
        /// <remarks>
        ///     No defaults are used, the delegate must set all of the required <see cref="SeoOptions" />.
        /// </remarks>
        /// <param name="services">The <see cref="IServiceCollection" /> to register the services with.</param>
        /// <param name="configureOptions">A delegate that configures the <see cref="SeoOptions" />.</param>
        /// <returns>The <see cref="IServiceCollection" />.</returns>
        public static IServiceCollection AddSeo(
            this IServiceCollection services,
            Action<SeoOptions, IWebHostEnvironment> configureOptions)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddOptions<SeoOptions>().Configure(configureOptions);
            services
                .AddControllers(options => options.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter()))
                .AddApplicationPart(Assembly.GetExecutingAssembly());
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            return services
                .AddTransient<IRobotsTxtFactory, RobotsTxtFactory>()
                .AddTransient<ISitemapFactory, SitemapFactory>()
                .AddSingleton<ITagHelperComponent, OpenGraphNamespaceTagHelperComponent>();
        }

        /// <summary>
        ///     Adds the required services for the SEO controller and OpenGraph tag helper.
        ///     Configures an empty sitemap and uses the default <see cref="RobotsTxtOptions" />.
        /// </summary>
        /// <remarks>
        ///     By default the <see cref="RobotsTxtOptions" /> are configured to only allow robots in production.
        /// </remarks>
        /// <param name="services">The <see cref="IServiceCollection" /> to register the services with.</param>
        /// <returns>The <see cref="IServiceCollection" />.</returns>
        public static IServiceCollection AddSeoWithDefaultRobots(this IServiceCollection services)
        {
            return services.AddSeoWithDefaultRobots(_ => { });
        }

        /// <summary>
        ///     Adds the required services for the SEO controller and OpenGraph tag helper.
        ///     Configures the <see cref="SitemapOptions" /> using a delegate and uses the default <see cref="RobotsTxtOptions" />.
        /// </summary>
        /// <remarks>
        ///     By default the <see cref="RobotsTxtOptions" /> are configured to only allow robots in production.
        /// </remarks>
        /// <param name="services">The <see cref="IServiceCollection" /> to register the services with.</param>
        /// <param name="configureSitemap">A delegate that configures the <see cref="SitemapOptions" />.</param>
        /// <returns>The <see cref="IServiceCollection" />.</returns>
        public static IServiceCollection AddSeoWithDefaultRobots(
            this IServiceCollection services,
            Action<SitemapOptions> configureSitemap)
        {
            return services.AddSeoWithDefaultRobots((options, _) => configureSitemap(options));
        }

        /// <summary>
        ///     Adds the required services for the SEO controller and OpenGraph tag helper.
        ///     Configures the <see cref="SitemapOptions" /> using a delegate which has access to the
        ///     <see cref="IWebHostEnvironment" /> and uses the default <see cref="RobotsTxtOptions" />.
        /// </summary>
        /// <remarks>
        ///     By default the <see cref="RobotsTxtOptions" /> are configured to only allow robots in production.
        /// </remarks>
        /// <param name="services">The <see cref="IServiceCollection" /> to register the services with.</param>
        /// <param name="configureSitemap">A delegate that configures the <see cref="SitemapOptions" />.</param>
        /// <returns>The <see cref="IServiceCollection" />.</returns>
        public static IServiceCollection AddSeoWithDefaultRobots(
            this IServiceCollection services,
            Action<SitemapOptions, IWebHostEnvironment> configureSitemap)
        {
            return services.AddSeo(
                (options, env) =>
                {
                    DefaultOptions.Configure(options, env);
                    configureSitemap(options.Sitemap, env);
                });
        }
    }
}