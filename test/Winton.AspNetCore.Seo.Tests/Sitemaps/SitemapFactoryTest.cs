using System.Collections.Generic;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace Winton.AspNetCore.Seo.Sitemaps
{
    public class SitemapFactoryTest
    {
        private readonly HttpContextAccessor _httpContextAccessor;
        private readonly Mock<IOptionsSnapshot<SeoOptions>> _optionsSnapshot;
        private readonly SitemapFactory _sitemapFactory;

        public SitemapFactoryTest()
        {
            _httpContextAccessor = new HttpContextAccessor();
            _optionsSnapshot = new Mock<IOptionsSnapshot<SeoOptions>>(MockBehavior.Strict);
            _sitemapFactory = new SitemapFactory(_optionsSnapshot.Object, _httpContextAccessor);
        }

        public sealed class Create : SitemapFactoryTest
        {
            [Fact]
            private void ShouldCreateSitemap()
            {
                _optionsSnapshot
                    .Setup(os => os.Value)
                    .Returns(
                        new SeoOptions
                        {
                            Sitemap =
                            {
                                Urls = new List<SitemapUrlOptions>
                                {
                                    new SitemapUrlOptions
                                    {
                                        Priority = 0.9M,
                                        RelativeUrl = "/login"
                                    }
                                }
                            }
                        });
                _httpContextAccessor.HttpContext = new DefaultHttpContext
                {
                    Request =
                    {
                        Host = new HostString("example.com", 5000),
                        Path = "/app-root/sitemap.xml",
                        Scheme = "https"
                    }
                };

                Sitemap sitemap = _sitemapFactory.Create();

                sitemap
                    .Should()
                    .BeEquivalentTo(
                        new Sitemap(
                            new List<SitemapUrl>
                            {
                                new SitemapUrl
                                {
                                    Priority = 0.9M,
                                    Location = "https://example.com:5000/app-root/login"
                                }
                            }));
            }
        }
    }
}