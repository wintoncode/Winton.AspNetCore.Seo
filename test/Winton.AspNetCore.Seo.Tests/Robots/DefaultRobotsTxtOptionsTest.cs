using System.Linq;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using Xunit;

namespace Winton.AspNetCore.Seo.Robots
{
    public class DefaultRobotsTxtOptionsTest
    {
        private const string _DevelopmentEnvironmentName = "Development";
        private const string _ProductionEnvironmentName = "Production";
        private const string _StagingEnvironmentName = "Staging";

        public sealed class AddSitemapUrl : DefaultRobotsTxtOptionsTest
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
                var defaultRobotsTxtOptions = new DefaultRobotsTxtOptions(hostingEnvironment);

                defaultRobotsTxtOptions.AddSitemapUrl.Should().BeTrue();
            }
        }

        public sealed class UserAgentRecords : DefaultRobotsTxtOptionsTest
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
                var defaultRobotsTxtOptions = new DefaultRobotsTxtOptions(hostingEnvironment);

                UserAgentRecord userAgentRecord = defaultRobotsTxtOptions.UserAgentRecords.Single();

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