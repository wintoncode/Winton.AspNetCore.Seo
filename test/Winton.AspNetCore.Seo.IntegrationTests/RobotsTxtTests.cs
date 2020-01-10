using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Winton.AspNetCore.Seo.Robots;
using Xunit;

namespace Winton.AspNetCore.Seo
{
    public class RobotsTxtTests
    {
        public static IEnumerable<object?[]> TestCases => new List<object?[]>
        {
            new object?[]
            {
                typeof(MvcStartup),
                new StringBuilder()
                    .AppendLine("User-agent: *")
                    .AppendLine("Disallow: ")
                    .AppendLine()
                    .AppendLine("Sitemap: http://localhost/sitemap.xml")
                    .ToString()
            },
            new object?[]
            {
                typeof(DefaultSeoStartup),
                new StringBuilder()
                    .AppendLine("User-agent: *")
                    .AppendLine("Disallow: ")
                    .AppendLine()
                    .AppendLine("Sitemap: http://localhost/sitemap.xml")
                    .ToString()
            },
            new object?[]
            {
                typeof(ConfigureRobotsDelegateStartup),
                new StringBuilder()
                    .AppendLine("User-agent: bing")
                    .AppendLine("Disallow: /login")
                    .AppendLine()
                    .ToString()
            },
            new object?[]
            {
                typeof(SeoFromConfigStartup),
                new StringBuilder()
                    .AppendLine("User-agent: google")
                    .AppendLine("Disallow: /about")
                    .AppendLine()
                    .ToString()
            }
        };

        [Theory]
        [MemberData(nameof(TestCases))]
        private async Task ShouldProduceCorrectRobotsTxt(Type startup, string expected)
        {
            IHost host = await new HostBuilder()
                .ConfigureWebHost(
                    builder => builder
                        .UseTestServer()
                        .UseStartup(startup))
                .StartAsync();
            using HttpClient client = host.GetTestClient();
            HttpResponseMessage response = await client.GetAsync("robots.txt");
            string responseString = await response.Content.ReadAsStringAsync();

            responseString.Should().BeEquivalentTo(expected);
        }

        private class ConfigureRobotsDelegateStartup
        {
            public void Configure(IApplicationBuilder app)
            {
                app
                    .UseRouting()
                    .UseEndpoints(builder => builder.MapDefaultControllerRoute());
            }

            public void ConfigureServices(IServiceCollection services)
            {
                services.AddSeo(
                    options =>
                    {
                        options.RobotsTxt.AddSitemapUrl = false;
                        options.RobotsTxt.UserAgentRecords = new List<UserAgentRecord>
                        {
                            new UserAgentRecord
                            {
                                Disallow = new List<string> { "/login" },
                                UserAgent = "bing"
                            }
                        };
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
                                { "Seo:RobotsTxt:AddSitemapUrl", "false" },
                                { "Seo:RobotsTxt:UserAgentRecords:0:UserAgent", "google" },
                                { "Seo:RobotsTxt:UserAgentRecords:0:Disallow:0", "/about" }
                            })
                        .Build();

                services.AddSeo(configuration);
            }
        }
    }
}