using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace Winton.AspNetCore.Seo
{
    public class SeoControllerIntegrationTests
    {
        private const string _RobotsEndpoint = "robots.txt";
        private const string _SitemapEndpoint = "sitemap.xml";
        private readonly TestServer _server;

        public SeoControllerIntegrationTests()
        {
            _server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
        }

        [Fact]
        private async Task ShouldHaveRobotsEndpoint()
        {
            using HttpClient client = _server.CreateClient();
            HttpResponseMessage response = await client.GetAsync(_RobotsEndpoint);

            response.IsSuccessStatusCode.Should().BeTrue();
        }

        [Fact]
        private async Task ShouldHaveSitemapEndpoint()
        {
            using HttpClient client = _server.CreateClient();
            HttpResponseMessage response = await client.GetAsync(_SitemapEndpoint);

            response.IsSuccessStatusCode.Should().BeTrue();
        }

        [Fact]
        private async Task ShouldSerialiseSitemapToXmlCorrectly()
        {
            using HttpClient client = _server.CreateClient();
            HttpResponseMessage response = await client.GetAsync(_SitemapEndpoint);
            string responseString = await response.Content.ReadAsStringAsync();

            responseString.Should().BeEquivalentTo(
                "<urlset xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\"><url><loc>http://localhost/test</loc><priority>0.9</priority></url></urlset>");
        }
    }
}