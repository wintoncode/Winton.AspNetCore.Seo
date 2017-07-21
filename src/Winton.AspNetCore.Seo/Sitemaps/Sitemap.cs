using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Winton.AspNetCore.Seo.Sitemaps
{
    [CollectionDataContract(Name = "urlset", Namespace = "http://www.sitemaps.org/schemas/sitemap/0.9")]
    public sealed class Sitemap : List<SitemapUrl>
    {
        public Sitemap()
        {
        }

        public Sitemap(IEnumerable<SitemapUrl> collection)
            : base(collection)
        {
        }
    }
}
