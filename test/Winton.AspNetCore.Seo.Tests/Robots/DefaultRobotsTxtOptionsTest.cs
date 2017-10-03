using System.Linq;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting.Internal;
using Winton.AspNetCore.Seo.Robots;
using Xunit;

namespace Winton.AspNetCore.Seo.Tests.Robots
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
                var hostingEnvironment = new HostingEnvironment
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
                var hostingEnvironment = new HostingEnvironment
                {
                    EnvironmentName = environmentName
                };
                var defaultRobotsTxtOptions = new DefaultRobotsTxtOptions(hostingEnvironment);

                UserAgentRecord userAgentRecord = defaultRobotsTxtOptions.UserAgentRecords.Single();

                userAgentRecord.DisallowAll.Should().Be(expected);
            }
        }
    }
}