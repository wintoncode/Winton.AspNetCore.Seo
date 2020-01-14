using System.Linq;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using Xunit;

namespace Winton.AspNetCore.Seo.Robots
{
    public class DefaultOptionsTest
    {
        private const string _DevelopmentEnvironmentName = "Development";
        private const string _ProductionEnvironmentName = "Production";
        private const string _StagingEnvironmentName = "Staging";

        public sealed class AddSitemapUrl : DefaultOptionsTest
        {
            [Theory]
            [InlineData(_ProductionEnvironmentName)]
            [InlineData(_StagingEnvironmentName)]
            [InlineData(_DevelopmentEnvironmentName)]
            private void ShouldReturnTrueForAllEnvironments(string environmentName)
            {
                var hostingEnvironment = new TestHostingEnvironment
                {
                    EnvironmentName = environmentName
                };
                var options = new SeoOptions();
                DefaultOptions.Configure(options, hostingEnvironment);

                options.RobotsTxt.AddSitemapUrl.Should().BeTrue();
            }
        }

        public sealed class UserAgentRecords : DefaultOptionsTest
        {
            [Theory]
            [InlineData(_ProductionEnvironmentName, false)]
            [InlineData(_StagingEnvironmentName, true)]
            [InlineData(_DevelopmentEnvironmentName, true)]
            private void ShouldAllowSiteWideAccessInProduction(string environmentName, bool expected)
            {
                var hostingEnvironment = new TestHostingEnvironment
                {
                    EnvironmentName = environmentName
                };
                var options = new SeoOptions();
                DefaultOptions.Configure(options, hostingEnvironment);

                UserAgentRecord userAgentRecord = options.RobotsTxt.UserAgentRecords.Single();

                userAgentRecord.DisallowAll.Should().Be(expected);
            }
        }

        private class TestHostingEnvironment : IWebHostEnvironment
        {
            public string ApplicationName { get; set; } = string.Empty;

            public IFileProvider ContentRootFileProvider { get; set; } = new NullFileProvider();

            public string ContentRootPath { get; set; } = string.Empty;

            public string EnvironmentName { get; set; } = string.Empty;

            public IFileProvider WebRootFileProvider { get; set; } = new NullFileProvider();

            public string WebRootPath { get; set; } = string.Empty;
        }
    }
}