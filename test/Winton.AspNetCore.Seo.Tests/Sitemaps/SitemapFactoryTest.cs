using System.Collections.Generic;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using Winton.AspNetCore.Seo.Sitemaps;
using Xunit;

namespace Winton.AspNetCore.Seo.Tests.Sitemaps
{
    public class SitemapFactoryTest
    {
        public sealed class Create : SitemapFactoryTest
        {
            [Fact]
            private void ShouldCreateSitemap()
            {
                var sitemapConfigUrl = new SitemapConfigUrl
                {
                    Priority = 0.9M,
                    RelativeUrl = "/login"
                };
                var configMock = new Mock<ISitemapConfig>(MockBehavior.Strict);
                configMock.SetupGet(cm => cm.Urls).Returns(new List<SitemapConfigUrl> { sitemapConfigUrl });
                var httpContextAccessor = new HttpContextAccessor
                {
                    HttpContext = new DefaultHttpContext
                    {
                        Request =
                        {
                            Host = new HostString("example.com", 5000),
                            Path = "/app-root/sitemap.xml",
                            Scheme = "https"
                        }
                    }
                };

                var sitemapFactory = new SitemapFactory(configMock.Object, httpContextAccessor);
                var expected = new Sitemap(
                    new List<SitemapUrl>
                    {
                        new SitemapUrl
                        {
                            Priority = 0.9M,
                            Location = "https://example.com:5000/app-root/login"
                        }
                    });

                Sitemap sitemap = sitemapFactory.Create();

                sitemap.ShouldAllBeEquivalentTo(expected);
            }
        }
    }
}
