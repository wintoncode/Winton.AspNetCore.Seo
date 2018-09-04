using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Winton.AspNetCore.Seo.Robots;
using Winton.AspNetCore.Seo.Sitemaps;
using Xunit;

namespace Winton.AspNetCore.Seo
{
    public class SeoControllerTest
    {
        public sealed class GetRobots : SeoControllerTest
        {
            private static ISitemapFactory GetSitemapFactory()
            {
                return new Mock<ISitemapFactory>(MockBehavior.Strict).Object;
            }

            [Fact]
            private void ShouldReturnContentResult()
            {
                var robotsFactoryMock = new Mock<IRobotsTxtFactory>(MockBehavior.Strict);
                robotsFactoryMock.Setup(rf => rf.Create()).Returns(string.Empty);

                var seoController = new SeoController(robotsFactoryMock.Object, GetSitemapFactory());

                IActionResult robotsResult = seoController.GetRobots();

                robotsResult.Should().BeOfType<ContentResult>();
            }

            [Fact]
            private void ShouldReturnCorrectContent()
            {
                var robotsFactoryMock = new Mock<IRobotsTxtFactory>(MockBehavior.Strict);
                robotsFactoryMock.Setup(rf => rf.Create()).Returns("User-agent: *");

                var seoController = new SeoController(robotsFactoryMock.Object, GetSitemapFactory());

                IActionResult robotsResult = seoController.GetRobots();

                robotsResult.As<ContentResult>().Content.Should().BeEquivalentTo("User-agent: *");
            }

            [Fact]
            private void ShouldReturnCorrectContentType()
            {
                var robotsFactoryMock = new Mock<IRobotsTxtFactory>(MockBehavior.Strict);
                robotsFactoryMock.Setup(rf => rf.Create()).Returns(string.Empty);

                var seoController = new SeoController(robotsFactoryMock.Object, GetSitemapFactory());

                IActionResult robotsResult = seoController.GetRobots();

                robotsResult.As<ContentResult>().ContentType.Should().BeEquivalentTo("text/plain; charset=utf-8");
            }
        }

        public sealed class GetSitemap : SeoControllerTest
        {
            private static IRobotsTxtFactory GetRobotsTxtFactory()
            {
                return new Mock<IRobotsTxtFactory>(MockBehavior.Strict).Object;
            }

            [Fact]
            private void ShouldReturnObjectResult()
            {
                var sitemap = new Sitemap();
                var sitemapFactoryMock = new Mock<ISitemapFactory>(MockBehavior.Strict);
                sitemapFactoryMock.Setup(sf => sf.Create()).Returns(sitemap);
                var seoController = new SeoController(GetRobotsTxtFactory(), sitemapFactoryMock.Object);

                IActionResult sitemapResult = seoController.GetSitemap();

                sitemapResult.Should().BeOfType<ObjectResult>();
            }

            [Fact]
            private void ShouldReturnSitemap()
            {
                var sitemap = new Sitemap();
                var sitemapFactoryMock = new Mock<ISitemapFactory>(MockBehavior.Strict);
                sitemapFactoryMock.Setup(sf => sf.Create()).Returns(sitemap);
                var seoController = new SeoController(GetRobotsTxtFactory(), sitemapFactoryMock.Object);

                IActionResult sitemapResult = seoController.GetSitemap();

                sitemapResult.As<ObjectResult>().Value.Should().BeSameAs(sitemap);
            }
        }
    }
}