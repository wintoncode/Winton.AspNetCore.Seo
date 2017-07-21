using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;

namespace Winton.AspNetCore.Seo.Sitemaps
{
    internal sealed class SitemapFactory : ISitemapFactory
    {
        private readonly ISitemapConfig _config;
        private readonly IHttpContextAccessor _contextAccessor;

        public SitemapFactory(ISitemapConfig config, IHttpContextAccessor contextAccessor)
        {
            _config = config;
            _contextAccessor = contextAccessor;
        }

        public Sitemap Create()
        {
            string baseUri = _contextAccessor.HttpContext.Request.GetEncodedUrl()
                                             .Replace(Constants.SitemapUrl, string.Empty);
            return new Sitemap(_config.Urls.Select(url => url.ToSitemapUrl(baseUri)));
        }
    }
}
