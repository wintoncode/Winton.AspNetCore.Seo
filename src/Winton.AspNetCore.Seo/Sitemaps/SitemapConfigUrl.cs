using Flurl;

namespace Winton.AspNetCore.Seo.Sitemaps
{
    public sealed class SitemapConfigUrl
    {
        public decimal? Priority { get; set; }

        public string RelativeUrl { get; set; }

        public SitemapUrl ToSitemapUrl(string baseUri)
        {
            return new SitemapUrl
            {
                Location = baseUri.AppendPathSegment(RelativeUrl),
                Priority = Priority
            };
        }
    }
}