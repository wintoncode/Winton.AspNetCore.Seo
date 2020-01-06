using System.Collections.Generic;
using FluentAssertions;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Xunit;

namespace Winton.AspNetCore.Seo.HeaderMetadata.OpenGraph.Music
{
    public class OpenGraphMusicRadioStationTagHelperTests
    {
        public sealed class Process : OpenGraphMusicRadioStationTagHelperTests
        {
            private static readonly MetaTag NamespaceMetaTag = new MetaTag(
                "OpenGraphNamespaceTagHelperComponent",
                "music: http://ogp.me/ns/music# og: http://ogp.me/ns#");

            private static readonly MetaTag TypeMetaTag = new MetaTag("og:type", "music.radio_station");

            public static IEnumerable<object[]> TestCases => new List<object[]>
            {
                new object[]
                {
                    new OpenGraphMusicRadioStationTagHelper(),
                    new List<MetaTag>
                    {
                        TypeMetaTag,
                        NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new OpenGraphMusicRadioStationTagHelper { Creator = "https://example.com" },
                    new List<MetaTag>
                    {
                        new MetaTag("music:creator", "https://example.com"),
                        TypeMetaTag,
                        NamespaceMetaTag
                    }
                }
            };

            [Theory]
            [MemberData(nameof(TestCases))]
            private void ShouldContainCorrectMetaTags(
                OpenGraphMusicRadioStationTagHelper tagHelper,
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

        public sealed class Type : OpenGraphMusicRadioStationTagHelperTests
        {
            [Fact]
            private void ShouldBeMusicRadioStation()
            {
                var tagHelper = new OpenGraphMusicRadioStationTagHelper();

                string? type = tagHelper.Type;

                type.Should().Be("music.radio_station");
            }
        }
    }
}