using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Winton.AspNetCore.Seo.HeaderMetadata.OpenGraph;
using Winton.AspNetCore.Seo.HeaderMetadata.OpenGraph.Music;
using Xunit;

namespace Winton.AspNetCore.Seo.Tests.HeaderMetadata.OpenGraph.Music
{
    public class OpenGraphMusicRadioStationTagHelperTests
    {
        public sealed class Process : OpenGraphMusicRadioStationTagHelperTests
        {
            private static readonly MetaTag _TypeMetaTag = new MetaTag("og:type", "music.radio_station");
            private static readonly MetaTag _NamespaceMetaTag = new MetaTag("OpenGraphNamespaceTagHelperComponent", "music: http://ogp.me/ns/music# og: http://ogp.me/ns#");

            public static IEnumerable<object[]> TestCases => new List<object[]>
            {
                new object[]
                {
                    new OpenGraphMusicRadioStationTagHelper(),
                    new List<MetaTag>
                    {
                        _TypeMetaTag,
                        _NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new OpenGraphMusicRadioStationTagHelper { Creator = "https://example.com" },
                    new List<MetaTag>
                    {
                        new MetaTag("music:creator", "https://example.com"),
                        _TypeMetaTag,
                        _NamespaceMetaTag
                    }
                }
            };

            [Theory]
            [MemberData(nameof(TestCases))]
            private void ShouldContainCorrectMetaTags(OpenGraphMusicRadioStationTagHelper tagHelper, IEnumerable<MetaTag> expectedMetaTags)
            {
                var context = TagHelperTestUtils.CreateDefaultContext();
                var output = TagHelperTestUtils.CreateDefaultOutput();

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

                string type = tagHelper.Type;

                type.Should().Be("music.radio_station");
            }
        }
    }
}
