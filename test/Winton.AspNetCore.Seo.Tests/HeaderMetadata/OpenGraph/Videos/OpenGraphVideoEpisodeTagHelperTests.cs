using System.Collections.Generic;
using FluentAssertions;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Xunit;

namespace Winton.AspNetCore.Seo.HeaderMetadata.OpenGraph.Videos
{
    public class OpenGraphVideoEpisodeTagHelperTests
    {
        public sealed class Process : OpenGraphVideoEpisodeTagHelperTests
        {
            private static readonly MetaTag NamespaceMetaTag = new MetaTag(
                "OpenGraphNamespaceTagHelperComponent",
                "og: http://ogp.me/ns# video: http://ogp.me/ns/video#");

            private static readonly MetaTag TypeMetaTag = new MetaTag("og:type", "video.episode");

            public static IEnumerable<object[]> TestCases => new List<object[]>
            {
                new object[]
                {
                    new OpenGraphVideoEpisodeTagHelper(),
                    new List<MetaTag>
                    {
                        TypeMetaTag,
                        NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new OpenGraphVideoEpisodeTagHelper { Series = "http://example.com" },
                    new List<MetaTag>
                    {
                        new MetaTag("video:series", "http://example.com"),
                        TypeMetaTag,
                        NamespaceMetaTag
                    }
                }
            };

            [Theory]
            [MemberData(nameof(TestCases))]
            private void ShouldContainCorrectMetaTags(
                OpenGraphVideoEpisodeTagHelper tagHelper,
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

        public sealed class Type : OpenGraphVideoEpisodeTagHelperTests
        {
            [Fact]
            private void ShouldBeVideoEpisode()
            {
                var tagHelper = new OpenGraphVideoEpisodeTagHelper();

                string? type = tagHelper.Type;

                type.Should().Be("video.episode");
            }
        }
    }
}