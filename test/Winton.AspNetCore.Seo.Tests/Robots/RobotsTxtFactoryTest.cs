using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace Winton.AspNetCore.Seo.Robots
{
    public class RobotsTxtFactoryTest
    {
        private readonly HttpContextAccessor _httpContextAccessor;
        private readonly Mock<IOptionsSnapshot<SeoOptions>> _optionsSnapshot;
        private readonly RobotsTxtFactory _robotsFactory;

        public RobotsTxtFactoryTest()
        {
            _optionsSnapshot = new Mock<IOptionsSnapshot<SeoOptions>>(MockBehavior.Strict);
            _httpContextAccessor = new HttpContextAccessor();
            _robotsFactory = new RobotsTxtFactory(_optionsSnapshot.Object, _httpContextAccessor);
        }

        public sealed class Create : RobotsTxtFactoryTest
        {
            [Fact]
            private void ShouldAddAllUserAgentRecordsSeperatedByABlankLine()
            {
                _optionsSnapshot
                    .Setup(os => os.Value)
                    .Returns(
                        new SeoOptions
                        {
                            RobotsTxt =
                            {
                                AddSitemapUrl = false,
                                UserAgentRecords = new List<UserAgentRecord>
                                {
                                    new UserAgentRecord
                                    {
                                        UserAgent = "Google"
                                    },
                                    new UserAgentRecord
                                    {
                                        UserAgent = "Bing"
                                    }
                                }
                            }
                        });

                string robotsTxt = _robotsFactory.Create();

                robotsTxt
                    .Should()
                    .Contain(
                        new StringBuilder()
                            .AppendLine("User-agent: Google")
                            .AppendLine("Disallow: ")
                            .AppendLine()
                            .AppendLine("User-agent: Bing")
                            .AppendLine("Disallow: ")
                            .AppendLine()
                            .ToString());
            }

            [Fact]
            private void ShouldHaveCorrectSitemapUrl()
            {
                _httpContextAccessor.HttpContext = new DefaultHttpContext
                {
                    Request =
                    {
                        Host = new HostString("example.com", 5000),
                        Path = "/app-root/robots.txt",
                        Scheme = "https"
                    }
                };
                _optionsSnapshot
                    .Setup(os => os.Value)
                    .Returns(
                        new SeoOptions
                        {
                            RobotsTxt =
                            {
                                AddSitemapUrl = true
                            }
                        });

                string robotsTxt = _robotsFactory.Create();

                robotsTxt.Should().Contain("Sitemap: https://example.com:5000/app-root/sitemap.xml");
            }

            [Theory]
            [InlineData(false, false)]
            [InlineData(true, true)]
            private void ShouldIncludeSitemapUrl(bool addsiteMapUrl, bool expected)
            {
                _optionsSnapshot
                    .Setup(os => os.Value)
                    .Returns(
                        new SeoOptions
                        {
                            RobotsTxt =
                            {
                                AddSitemapUrl = addsiteMapUrl
                            }
                        });

                string robotsTxt = _robotsFactory.Create();

                robotsTxt.Contains("Sitemap").Should().Be(expected);
            }

            [Fact]
            private void ShouldNotAddAnyUserAgentRecordsIfEmpty()
            {
                _optionsSnapshot
                    .Setup(os => os.Value)
                    .Returns(
                        new SeoOptions
                        {
                            RobotsTxt =
                            {
                                AddSitemapUrl = false,
                                UserAgentRecords = new List<UserAgentRecord>()
                            }
                        });

                string robotsTxt = _robotsFactory.Create();

                robotsTxt.Should().NotContain("User-agent");
            }

            [Fact]
            private void ShouldNotAddAnyUserAgentRecordsIfNull()
            {
                _optionsSnapshot
                    .Setup(os => os.Value)
                    .Returns(
                        new SeoOptions
                        {
                            RobotsTxt =
                            {
                                AddSitemapUrl = false
                            }
                        });

                string robotsTxt = _robotsFactory.Create();

                robotsTxt.Should().NotContain("User-agent");
            }
        }
    }
}