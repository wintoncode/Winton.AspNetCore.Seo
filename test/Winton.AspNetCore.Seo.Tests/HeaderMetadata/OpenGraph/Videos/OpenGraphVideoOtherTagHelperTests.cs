using System.Collections.Generic;
using FluentAssertions;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Winton.AspNetCore.Seo.HeaderMetadata.OpenGraph;
using Winton.AspNetCore.Seo.HeaderMetadata.OpenGraph.Videos;
using Xunit;

namespace Winton.AspNetCore.Seo.Tests.HeaderMetadata.OpenGraph.Videos
{
    public class OpenGraphVideoOtherTagHelperTests
    {
        public sealed class Process : OpenGraphVideoOtherTagHelperTests
        {
            private static readonly MetaTag NamespaceMetaTag = new MetaTag(
                "OpenGraphNamespaceTagHelperComponent",
                "og: http://ogp.me/ns# video: http://ogp.me/ns/video#");

            private static readonly MetaTag TypeMetaTag = new MetaTag("og:type", "video.other");

            public static IEnumerable<object[]> TestCases => new List<object[]>
            {
                new object[]
                {
                    new OpenGraphVideoOtherTagHelper(),
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
                OpenGraphVideoOtherTagHelper tagHelper,
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

        public sealed class Type : OpenGraphVideoOtherTagHelperTests
        {
            [Fact]
            private void ShouldBeVideoOther()
            {
                var tagHelper = new OpenGraphVideoOtherTagHelper();

                string type = tagHelper.Type;

                type.Should().Be("video.other");
            }
        }
    }
}