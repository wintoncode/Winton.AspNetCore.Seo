using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Winton.AspNetCore.Seo.Extensions
{
    public static class RouteBuilderExtensions
    {
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
