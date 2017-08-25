using FluentAssertions;
using Winton.AspNetCore.Seo.Robots;
using Xunit;

namespace Winton.AspNetCore.Seo.Tests.Robots
{
    public class UserAgentTest
    {
        public sealed class Any : UserAgentTest
        {
            [Fact]
            private void ShouldReturnCorrectValue()
            {
                UserAgent.Any.Should().Be((UserAgent)"*");
            }
        }
    }
}