using System.Text;
using Flurl;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;

namespace Winton.AspNetCore.Seo.Robots
{
    internal sealed class RobotsTxtFactory : IRobotsTxtFactory
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RobotsTxtFactory(IHostingEnvironment hostingEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _hostingEnvironment = hostingEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }

        public string Create()
        {
            StringBuilder stringBuilder = new StringBuilder()
                .AppendLine("User-agent: *");

            if (!_hostingEnvironment.IsProduction())
            {
                stringBuilder.AppendLine("Disallow: /");
            }
            else
            {
                string baseUrl = _httpContextAccessor?.HttpContext?.Request?.GetEncodedUrl();
                Url sitemapUrl = (baseUrl ?? string.Empty).Replace(Constants.RobotsUrl, Constants.SitemapUrl);
                stringBuilder.AppendLine($"GetSitemap: {sitemapUrl}");
            }

            return stringBuilder.ToString();
        }
    }
}
