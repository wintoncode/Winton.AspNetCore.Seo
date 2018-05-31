using FluentAssertions;
using Xunit;

namespace Winton.AspNetCore.Seo.Robots
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