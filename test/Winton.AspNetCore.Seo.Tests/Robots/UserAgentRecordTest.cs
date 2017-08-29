using System.Collections.Generic;
using FluentAssertions;
using Winton.AspNetCore.Seo.Robots;
using Xunit;

namespace Winton.AspNetCore.Seo.Tests.Robots
{
    public class UserAgentRecordTest
    {
        public sealed class CreateRecord : UserAgentRecordTest
        {
            [Fact]
            private void ShouldAllowAllRoutesIfDisallowNotSet()
            {
                var userAgentRecord = new UserAgentRecord();

                string section = userAgentRecord.CreateRecord();

                section.Should().Contain("Disallow:");
                section.Should().NotContain("Disallow: /");
            }

            [Fact]
            private void ShouldBlockAllRoutesIfDisallowAllTrue()
            {
                var userAgentRecord = new UserAgentRecord
                {
                    DisallowAll = true,
                    Disallow = new List<string> { "/test" }
                };

                string section = userAgentRecord.CreateRecord();

                section.Should().Contain("Disallow: /");
            }

            [Fact]
            private void ShouldBlockRoutesDefinedInDisallow()
            {
                var userAgentRecord = new UserAgentRecord
                {
                    Disallow = new List<string> { "/test", "/foo" }
                };

                string section = userAgentRecord.CreateRecord();

                section.Should().Contain("Disallow: /test");
                section.Should().Contain("Disallow: /foo");
            }

            [Theory]
            [InlineData("Google")]
            [InlineData("Bing")]
            private void ShouldSetCorrectUserAgent(string userAgent)
            {
                var userAgentRecord = new UserAgentRecord
                {
                    UserAgent = (UserAgent)userAgent
                };

                string section = userAgentRecord.CreateRecord();

                section.Should().Contain($"User-agent: {userAgent}");
            }
        }

        public sealed class UserAgentProperty : UserAgentRecordTest
        {
            [Fact]
            private void UserAgentShouldDefaultToAllowAny()
            {
                var userAgentRecord = new UserAgentRecord();
                userAgentRecord.UserAgent.Should().Be(UserAgent.Any);
            }
        }
    }
}