using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using Winton.AspNetCore.Seo.Robots;
using Xunit;

namespace Winton.AspNetCore.Seo.Tests.Robots
{
    public class RobotsTxtFactoryTest
    {
        private IRobotsTxtOptions CreateRobotsTxtOptions(
            bool addSitemapUrl,
            IEnumerable<UserAgentRecord> userAgentRecords = null)
        {
            var mock = new Mock<IRobotsTxtOptions>(MockBehavior.Strict);
            mock.SetupGet(rto => rto.AddSitemapUrl).Returns(addSitemapUrl);
            mock.SetupGet(rto => rto.UserAgentRecords).Returns(userAgentRecords);
            return mock.Object;
        }

        public sealed class Create : RobotsTxtFactoryTest
        {
            [Fact]
            private void ShouldAddAllUserAgentRecords()
            {
                var robotsFactory = new RobotsTxtFactory(
                    new HttpContextAccessor(),
                    CreateRobotsTxtOptions(
                        false,
                        new List<UserAgentRecord>
                        {
                            new UserAgentRecord
                            {
                                UserAgent = (UserAgent)"Google"
                            },
                            new UserAgentRecord
                            {
                                UserAgent = (UserAgent)"Bing"
                            }
                        }));

                string robotsTxt = robotsFactory.Create();

                string expected = new StringBuilder()
                    .AppendLine("User-agent: Google")
                    .AppendLine("Disallow: ")
                    .AppendLine()
                    .AppendLine("User-agent: Bing")
                    .AppendLine("Disallow: ")
                    .AppendLine()
                    .ToString();
                robotsTxt.Should().Contain(expected);
            }

            [Fact]
            private void ShouldHaveCorrectSitemapUrl()
            {
                var httpContextAccessor = new HttpContextAccessor
                {
                    HttpContext = new DefaultHttpContext
                    {
                        Request =
                        {
                            Host = new HostString("example.com", 5000),
                            Path = "/app-root/robots.txt",
                            Scheme = "https"
                        }
                    }
                };
                var robotsFactory = new RobotsTxtFactory(
                    httpContextAccessor,
                    CreateRobotsTxtOptions(true));

                string robotsTxt = robotsFactory.Create();

                robotsTxt.Should().Contain("GetSitemap: https://example.com:5000/app-root/sitemap.xml");
            }

            [Theory]
            [InlineData(false, false)]
            [InlineData(true, true)]
            private void ShouldIncludeSitemapUrl(bool addsiteMapUrl, bool expected)
            {
                var robotsFactory = new RobotsTxtFactory(
                    new HttpContextAccessor(),
                    CreateRobotsTxtOptions(addsiteMapUrl));

                string robotsTxt = robotsFactory.Create();

                robotsTxt.Contains("GetSitemap").Should().Be(expected);
            }

            [Fact]
            private void ShouldNotAddAnyUserAgentRecordsIfEmpty()
            {
                var robotsFactory = new RobotsTxtFactory(
                    new HttpContextAccessor(),
                    CreateRobotsTxtOptions(false, new List<UserAgentRecord>()));

                string robotsTxt = robotsFactory.Create();

                robotsTxt.Should().NotContain("User-agent");
            }

            [Fact]
            private void ShouldNotAddAnyUserAgentRecordsIfNull()
            {
                var robotsFactory = new RobotsTxtFactory(new HttpContextAccessor(), CreateRobotsTxtOptions(false));

                string robotsTxt = robotsFactory.Create();

                robotsTxt.Should().NotContain("User-agent");
            }
        }
    }
}