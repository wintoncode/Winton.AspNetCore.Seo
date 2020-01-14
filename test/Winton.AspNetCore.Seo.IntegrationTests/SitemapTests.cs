using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Winton.AspNetCore.Seo.Sitemaps;
using Xunit;

namespace Winton.AspNetCore.Seo
{
    public class SitemapTests
    {
        public static IEnumerable<object?[]> TestCases => new List<object?[]>
        {
            new object?[]
            {
                typeof(MvcStartup),
                "<urlset xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\" />"
            },
            new object?[]
            {
                typeof(DefaultSeoStartup),
                "<urlset xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\" />"
            },
            new object?[]
            {
                typeof(ConfigureSitemapDelegateStartup),
                "<urlset xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\"><url><loc>http://localhost/test</loc><priority>0.9</priority></url></urlset>"
            },
            new object?[]
            {
                typeof(SeoFromConfigStartup),
                "<urlset xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\"><url><loc>http://localhost/home</loc><priority>1.0</priority></url></urlset>"
            }
        };

        [Theory]
        [MemberData(nameof(TestCases))]
        private async Task ShouldProduceCorrectSitemap(Type startup, string expected)
        {
            IHost host = await new HostBuilder()
                .ConfigureWebHost(
                    builder => builder
                        .UseTestServer()
                        .UseStartup(startup))
                .StartAsync();
            using HttpClient client = host.GetTestClient();
            HttpResponseMessage response = await client.GetAsync("sitemap.xml");
            string responseString = await response.Content.ReadAsStringAsync();

            responseString.Should().BeEquivalentTo(expected);
        }

        private class ConfigureSitemapDelegateStartup
        {
            public void Configure(IApplicationBuilder app)
            {
                app
                    .UseRouting()
                    .UseEndpoints(builder => builder.MapDefaultControllerRoute());
            }

            public void ConfigureServices(IServiceCollection services)
            {
                services.AddSeoWithDefaultRobots(
                    options => options.Urls = new List<SitemapUrlOptions>
                    {
                        new SitemapUrlOptions
                        {
                            Priority = 0.9M,
                            RelativeUrl = "test"
                        }
                    });
            }
        }

        private class DefaultSeoStartup
        {
            public void Configure(IApplicationBuilder app)
            {
                app
                    .UseRouting()
                    .UseEndpoints(builder => builder.MapDefaultControllerRoute());
            }

            public void ConfigureServices(IServiceCollection services)
            {
                services.AddSeoWithDefaultRobots();
            }
        }

        private class SeoFromConfigStartup
        {
            public void Configure(IApplicationBuilder app)
            {
                app
                    .UseRouting()
                    .UseEndpoints(builder => builder.MapDefaultControllerRoute());
            }

            public void ConfigureServices(IServiceCollection services)
            {
                IConfigurationRoot configuration =
                    new ConfigurationBuilder()
                        .AddInMemoryCollection(
                            new Dictionary<string, string>
                            {
                                { "Seo:Sitemap:Urls:0:Priority", "1.0" },
                                { "Seo:Sitemap:Urls:0:RelativeUrl", "/home" }
                            })
                        .Build();

                services.AddSeo(configuration);
            }
        }

        private class MvcStartup
        {
            public void Configure(IApplicationBuilder app)
            {
                app
                    .UseMvc();
            }

            public void ConfigureServices(IServiceCollection services)
            {
                services
                    .AddSeoWithDefaultRobots()
                    .AddMvc(options => options.EnableEndpointRouting = false);
            }
        }
    }
}