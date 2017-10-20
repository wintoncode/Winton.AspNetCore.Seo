using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace Winton.AspNetCore.Seo.IntegrationTests
{
    public class SeoControllerIntegrationTest
    {
        private const string _RobotsEndpoint = "robots.txt";
        private const string _SitemapEndpoint = "sitemap.xml";
        private readonly HttpClient _client;

        public SeoControllerIntegrationTest()
        {
            var server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _client = server.CreateClient();
        }

        [Fact]
        private async Task ShouldHaveRobotsEndpoint()
        {
            HttpResponseMessage response = await _client.GetAsync(_RobotsEndpoint);

            response.IsSuccessStatusCode.Should().BeTrue();
        }

        [Fact]
        private async Task ShouldHaveSitemapEndpoint()
        {
            HttpResponseMessage response = await _client.GetAsync(_SitemapEndpoint);

            response.IsSuccessStatusCode.Should().BeTrue();
        }

        [Fact]
        private async Task ShouldSerialiseSitemapToXmlCorrectly()
        {
            HttpResponseMessage response = await _client.GetAsync(_SitemapEndpoint);
            string responseString = await response.Content.ReadAsStringAsync();

            responseString.ShouldBeEquivalentTo(
                "<urlset xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\"><url><loc>http://localhost/test</loc><priority>0.9</priority></url></urlset>");
        }
    }
}