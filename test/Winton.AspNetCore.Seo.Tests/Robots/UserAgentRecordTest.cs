using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace Winton.AspNetCore.Seo.Robots
{
    public class UserAgentRecordTest
    {
        public new sealed class ToString : UserAgentRecordTest
        {
            [Fact]
            private void ShouldAllowAllRoutesIfDisallowNotSet()
            {
                var userAgentRecord = new UserAgentRecord();

                string record = userAgentRecord.ToString();

                record.Should().Contain("Disallow:").And.NotContain("Disallow: /");
            }

            [Fact]
            private void ShouldBlockAllRoutesIfDisallowAllTrue()
            {
                var userAgentRecord = new UserAgentRecord
                {
                    DisallowAll = true,
                    Disallow = new List<string> { "/test" }
                };

                string record = userAgentRecord.ToString();

                record.Should().Contain("Disallow: /");
            }

            [Fact]
            private void ShouldBlockRoutesDefinedInDisallow()
            {
                var userAgentRecord = new UserAgentRecord
                {
                    Disallow = new List<string> { "/test", "/foo" }
                };

                string record = userAgentRecord.ToString();

                record.Should().Contain("Disallow: /test").And.Contain("Disallow: /foo");
            }

            [Fact]
            private void ShouldContainNoindexDirectiveIfDefinedForUrl()
            {
                var userAgentRecord = new UserAgentRecord
                {
                    NoIndex = new List<string> { "/test", "/foo" }
                };

                string record = userAgentRecord.ToString();

                record.Should().Contain("Noindex: /test").And.Contain("Noindex: /foo");
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

                string record = userAgentRecord.ToString();

                record.Should().Contain($"User-agent: {userAgent}");
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