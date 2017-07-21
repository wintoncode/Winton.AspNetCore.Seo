using System.Collections.Generic;

namespace Winton.AspNetCore.Seo.Sitemaps
{
    public interface ISitemapConfig
    {
        List<SitemapConfigUrl> Urls { get; }
    }
}