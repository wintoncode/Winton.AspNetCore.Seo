using FluentAssertions;
using Xunit;

namespace Winton.AspNetCore.Seo.HeaderMetadata.OpenGraph
{
    public class StringExtensionsTests
    {
        public sealed class ConvertTitleCaseToSnakeCase : StringExtensionsTests
        {
            [Theory]
            [InlineData("Test", "test")]
            [InlineData("TestTest", "test_test")]
            [InlineData("Testing123", "testing123")]
            private void ShouldCorrectlyTransformString(string input, string expected)
            {
                string output = input.ConvertTitleCaseToSnakeCase();

                output.Should().Be(expected);
            }
        }
    }
}