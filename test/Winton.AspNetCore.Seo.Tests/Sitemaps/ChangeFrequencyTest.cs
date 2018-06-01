using System.Reflection;
using System.Runtime.Serialization;
using FluentAssertions;
using Xunit;

namespace Winton.AspNetCore.Seo.Sitemaps
{
    public class ChangeFrequencyTest
    {
        [Fact]
        private void ShouldHaveDataContractAttribute()
        {
            typeof(ChangeFrequency).Should().BeDecoratedWith<DataContractAttribute>();
        }

        public class EnumMembers : ChangeFrequencyTest
        {
            [Theory]
            [InlineData(nameof(ChangeFrequency.Always), "always")]
            [InlineData(nameof(ChangeFrequency.Hourly), "hourly")]
            [InlineData(nameof(ChangeFrequency.Daily), "daily")]
            [InlineData(nameof(ChangeFrequency.Weekly), "weekly")]
            [InlineData(nameof(ChangeFrequency.Monthly), "monthly")]
            [InlineData(nameof(ChangeFrequency.Yearly), "yearly")]
            [InlineData(nameof(ChangeFrequency.Never), "never")]
            private void ShouldHaveCorrectValue(string memberName, string expectedValue)
            {
                typeof(ChangeFrequency).GetField(memberName)
                                       .GetCustomAttribute<EnumMemberAttribute>()
                                       .Value
                                       .Should()
                                       .Be(expectedValue);
            }

            [Theory]
            [InlineData(nameof(ChangeFrequency.Always))]
            [InlineData(nameof(ChangeFrequency.Hourly))]
            [InlineData(nameof(ChangeFrequency.Daily))]
            [InlineData(nameof(ChangeFrequency.Weekly))]
            [InlineData(nameof(ChangeFrequency.Monthly))]
            [InlineData(nameof(ChangeFrequency.Yearly))]
            [InlineData(nameof(ChangeFrequency.Never))]
            private void ShouldHaveEnumMemberAttribute(string memberName)
            {
                typeof(ChangeFrequency).GetField(memberName)
                                       .GetCustomAttributes<EnumMemberAttribute>()
                                       .Should()
                                       .NotBeEmpty();
            }
        }
    }
}