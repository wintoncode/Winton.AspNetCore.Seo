// Copyright (c) Winton. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Winton.AspNetCore.Seo.Extensions
{
    /// <summary>
    ///     Extension methods for <see cref="IRouteBuilder" /> which set up the MVC routes to serve the SEO files.
    /// </summary>
    public static class RouteBuilderExtensions
    {
        /// <summary>
        ///     Maps the routes in MVC used to serve SEO files, such as /robots.txt and /sitemap.xml.
        /// </summary>
        /// <param name="routeBuilder">The <see cref="IRouteBuilder" /> too add the routes to.</param>
        /// <returns>The instance of the <see cref="IRouteBuilder" /> that was passed in.</returns>
        public static IRouteBuilder MapSeoRoutes(this IRouteBuilder routeBuilder)
        {
            routeBuilder.MapRoute(
                "robots",
                Constants.RobotsUrl,
                new { controller = "Seo", action = nameof(SeoController.GetRobots) });
            routeBuilder.MapRoute(
                "sitemap",
                Constants.SitemapUrl,
                new { controller = "Seo", action = nameof(SeoController.GetSitemap) });
            return routeBuilder;
        }
    }
}