using FluentAssertions;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Winton.AspNetCore.Seo.Robots;
using Xunit;

namespace Winton.AspNetCore.Seo.Tests.Robots
{
    public class RobotsTxtFactoryTest
    {
        public sealed class Create : RobotsTxtFactoryTest
        {
            private const string _ProductionEnvironmentName = "Production";
            private const string _StagingEnvironmentName = "Staging";
            private const string _DevelopmentEnvironmentName = "Development";

            [Theory]
            [InlineData(_ProductionEnvironmentName)]
            [InlineData(_StagingEnvironmentName)]
            [InlineData(_DevelopmentEnvironmentName)]
            private void ShouldAllowAnyUserAgent(string environmentName)
            {
                var hostingEnvironment = new HostingEnvironment
                {
                    EnvironmentName = environmentName
                };
                var robotsFactory = new RobotsTxtFactory(hostingEnvironment, new HttpContextAccessor());

                string robotsTxt = robotsFactory.Create();

                robotsTxt.Contains("User-agent: *").Should().BeTrue();
            }

            [Theory]
            [InlineData(_ProductionEnvironmentName, false)]
            [InlineData(_StagingEnvironmentName, true)]
            [InlineData(_DevelopmentEnvironmentName, true)]
            private void ShouldDisallowAccess(string environmentName, bool expected)
            {
                var hostingEnvironment = new HostingEnvironment
                {
                    EnvironmentName = environmentName
                };
                var robotsFactory = new RobotsTxtFactory(hostingEnvironment, new HttpContextAccessor());

                string robotsTxt = robotsFactory.Create();

                robotsTxt.Contains("Disallow: /").Should().Be(expected);
            }

            [Theory]
            [InlineData(_ProductionEnvironmentName, true)]
            [InlineData(_StagingEnvironmentName, false)]
            [InlineData(_DevelopmentEnvironmentName, false)]
            private void ShouldIncludeSitemapUrl(string environmentName, bool expected)
            {
                var hostingEnvironment = new HostingEnvironment
                {
                    EnvironmentName = environmentName
                };
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
                var robotsFactory = new RobotsTxtFactory(hostingEnvironment, httpContextAccessor);

                string robotsTxt = robotsFactory.Create();

                robotsTxt.Contains("GetSitemap: https://example.com:5000/app-root/sitemap.xml").Should().Be(expected);
            }
        }
    }
}
