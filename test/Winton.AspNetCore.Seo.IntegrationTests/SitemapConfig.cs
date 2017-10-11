using System.Collections.Generic;
using Winton.AspNetCore.Seo.Sitemaps;

namespace Winton.AspNetCore.Seo.IntegrationTests
{
    internal sealed class SitemapConfig : ISitemapConfig
    {
        public List<SitemapConfigUrl> Urls { get; set; } = new List<SitemapConfigUrl>
        {
            new SitemapConfigUrl
            {
                Priority = 0.9M,
                RelativeUrl = "test"
            }
        };
    }
}