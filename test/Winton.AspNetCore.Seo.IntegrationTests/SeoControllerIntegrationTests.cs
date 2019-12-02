using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Winton.AspNetCore.Seo.Extensions;
using Winton.AspNetCore.Seo.Sitemaps;
using Xunit;

namespace Winton.AspNetCore.Seo
{
    public class SeoControllerIntegrationTests
    {
        private const string _RobotsEndpoint = "robots.txt";
        private const string _SitemapEndpoint = "sitemap.xml";

        public static IEnumerable<object?[]> HostBuilders => new List<object?[]>
        {
            new object?[]
            {
                ConfigureWebHost<MvcStartup>()
            },
            new object?[]
            {
                ConfigureWebHost<EndpointRoutingStartup>()
            }
        };

        private static IHostBuilder ConfigureWebHost<TStartup>()
            where TStartup : class
        {
            return new HostBuilder()
                .ConfigureWebHost(
                    builder => builder
                        .UseTestServer()
                        .UseStartup<TStartup>());
        }

        [Theory]
        [MemberData(nameof(HostBuilders))]
        private async Task ShouldHaveRobotsEndpoint(IHostBuilder hostBuilder)
        {
            IHost host = await hostBuilder.StartAsync();
            using HttpClient client = host.GetTestClient();
            HttpResponseMessage response = await client.GetAsync(_RobotsEndpoint);

            response.IsSuccessStatusCode.Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(HostBuilders))]
        private async Task ShouldHaveSitemapEndpoint(IHostBuilder hostBuilder)
        {
            IHost host = await hostBuilder.StartAsync();
            using HttpClient client = host.GetTestClient();
            HttpResponseMessage response = await client.GetAsync(_SitemapEndpoint);

            response.IsSuccessStatusCode.Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(HostBuilders))]
        private async Task ShouldSerialiseSitemapToXmlCorrectly(IHostBuilder hostBuilder)
        {
            IHost host = await hostBuilder.StartAsync();
            using HttpClient client = host.GetTestClient();
            HttpResponseMessage response = await client.GetAsync(_SitemapEndpoint);
            string responseString = await response.Content.ReadAsStringAsync();

            responseString.Should().BeEquivalentTo(
                "<urlset xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\"><url><loc>http://localhost/test</loc><priority>0.9</priority></url></urlset>");
        }

        private sealed class EndpointRoutingStartup
        {
            public void Configure(IApplicationBuilder app)
            {
                app
                    .UseRouting()
                    .UseEndpoints(builder => builder.MapDefaultControllerRoute());
            }

            public void ConfigureServices(IServiceCollection services)
            {
                services
                    .AddSeo<SitemapConfig>()
                    .AddControllersWithViews();
            }
        }

        private sealed class MvcStartup
        {
            public void Configure(IApplicationBuilder app)
            {
                app
                    .UseMvc();
            }

            public void ConfigureServices(IServiceCollection services)
            {
                services
                    .AddSeo<SitemapConfig>()
                    .AddMvc(options => options.EnableEndpointRouting = false);
            }
        }

        private sealed class SitemapConfig : ISitemapConfig
        {
            public List<SitemapConfigUrl> Urls { get; } = new List<SitemapConfigUrl>
            {
                new SitemapConfigUrl
                {
                    Priority = 0.9M,
                    RelativeUrl = "test"
                }
            };
        }
    }
}