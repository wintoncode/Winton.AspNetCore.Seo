using System.Collections.Generic;
using FluentAssertions;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Winton.AspNetCore.Seo.HeaderMetadata.OpenGraph;
using Xunit;

namespace Winton.AspNetCore.Seo.Tests.HeaderMetadata.OpenGraph
{
    public class OpenGraphWebsiteTagHelperTests
    {
        public sealed class Process : OpenGraphWebsiteTagHelperTests
        {
            private static readonly MetaTag NamespaceMetaTag = new MetaTag(
                "OpenGraphNamespaceTagHelperComponent",
                "og: http://ogp.me/ns#");

            private static readonly MetaTag TypeMetaTag = new MetaTag("og:type", "website");

            public static IEnumerable<object[]> TestCases => new List<object[]>
            {
                new object[]
                {
                    new OpenGraphWebsiteTagHelper(),
                    new List<MetaTag>
                    {
                        TypeMetaTag,
                        NamespaceMetaTag
                    }
                }
            };

            [Theory]
            [MemberData(nameof(TestCases))]
            private void ShouldContainCorrectMetaTags(
                OpenGraphWebsiteTagHelper tagHelper,
                IEnumerable<MetaTag> expectedMetaTags)
            {
                TagHelperContext context = TagHelperTestUtils.CreateDefaultContext();
                TagHelperOutput output = TagHelperTestUtils.CreateDefaultOutput();

                tagHelper.Process(context, output);

                output
                    .Should()
                    .HaveMetaTagsEquivalentTo(expectedMetaTags, options => options.WithStrictOrdering());
            }
        }

        public sealed class Type : OpenGraphWebsiteTagHelperTests
        {
            [Fact]
            private void ShouldBeWebsite()
            {
                var tagHelper = new OpenGraphWebsiteTagHelper();

                string type = tagHelper.Type;

                type.Should().Be("website");
            }
        }
    }
}