using System.Text;
using Microsoft.AspNetCore.Mvc;
using Winton.AspNetCore.Seo.Robots;
using Winton.AspNetCore.Seo.Sitemaps;

namespace Winton.AspNetCore.Seo
{
    public sealed class SeoController : Controller
    {
        private readonly IRobotsTxtFactory _robotsTxtFactory;
        private readonly ISitemapFactory _sitemapFactory;

        public SeoController(IRobotsTxtFactory robotsTxtFactory, ISitemapFactory sitemapFactory)
        {
            _robotsTxtFactory = robotsTxtFactory;
            _sitemapFactory = sitemapFactory;
        }

        public IActionResult GetRobots()
        {
            return Content(_robotsTxtFactory.Create(), "text/plain", Encoding.UTF8);
        }

        [Produces("text/xml")]
        public IActionResult GetSitemap()
        {
            Sitemap sitemap = _sitemapFactory.Create();
            return new ObjectResult(sitemap);
        }
    }
}