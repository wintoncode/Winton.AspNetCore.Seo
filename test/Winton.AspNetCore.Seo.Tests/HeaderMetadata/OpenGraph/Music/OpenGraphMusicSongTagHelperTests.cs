using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Winton.AspNetCore.Seo.HeaderMetadata.OpenGraph;
using Winton.AspNetCore.Seo.HeaderMetadata.OpenGraph.Music;
using Xunit;

namespace Winton.AspNetCore.Seo.Tests.HeaderMetadata.OpenGraph.Music
{
    public class OpenGraphMusicSongTagHelperTests
    {
        public sealed class Process : OpenGraphMusicSongTagHelperTests
        {
            private static readonly MetaTag _TypeMetaTag = new MetaTag("og:type", "music.song");
            private static readonly MetaTag _NamespaceMetaTag = new MetaTag("OpenGraphNamespaceTagHelperComponent", "music: http://ogp.me/ns/music# og: http://ogp.me/ns#");

            public static IEnumerable<object[]> TestCases => new List<object[]>
            {
                new object[]
                {
                    new OpenGraphMusicSongTagHelper(),
                    new List<MetaTag>
                    {
                        _TypeMetaTag,
                        _NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new OpenGraphMusicSongTagHelper
                    {
                        Albums = new List<Album> { new Album("https://example.com") }
                    },
                    new List<MetaTag>
                    {
                        new MetaTag("music:album", "https://example.com"),
                        _TypeMetaTag,
                        _NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new OpenGraphMusicSongTagHelper
                    {
                        Albums = new List<Album> { new Album("https://example.com") { Disc = 3 } }
                    },
                    new List<MetaTag>
                    {
                        new MetaTag("music:album", "https://example.com"),
                        new MetaTag("music:album:disc", "3"),
                        _TypeMetaTag,
                        _NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new OpenGraphMusicSongTagHelper
                    {
                        Albums = new List<Album> { new Album("https://example.com") { Track = 1 } }
                    },
                    new List<MetaTag>
                    {
                        new MetaTag("music:album", "https://example.com"),
                        new MetaTag("music:album:track", "1"),
                        _TypeMetaTag,
                        _NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new OpenGraphMusicSongTagHelper { Duration = 120 },
                    new List<MetaTag>
                    {
                        new MetaTag("music:duration", "120"),
                        _TypeMetaTag,
                        _NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new OpenGraphMusicSongTagHelper { Isrc = "abcde" },
                    new List<MetaTag>
                    {
                        new MetaTag("music:isrc", "abcde"),
                        _TypeMetaTag,
                        _NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new OpenGraphMusicSongTagHelper { Musicians = new List<string> { "https://example.com" } },
                    new List<MetaTag>
                    {
                        new MetaTag("music:musician", "https://example.com"),
                        _TypeMetaTag,
                        _NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new OpenGraphMusicSongTagHelper
                    {
                        PreviewUrl = new List<Audio> { new Audio("http://example.com") }
                    },
                    new List<MetaTag>
                    {
                        new MetaTag("music:preview_url", "http://example.com"),
                        _TypeMetaTag,
                        _NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new OpenGraphMusicSongTagHelper { ReleaseDate = new DateTime(2017, 1, 1) },
                    new List<MetaTag>
                    {
                        new MetaTag("music:release_date", "2017-01-01T00:00:00.0000000"),
                        _TypeMetaTag,
                        _NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new OpenGraphMusicSongTagHelper { ReleaseType = "original_release" },
                    new List<MetaTag>
                    {
                        new MetaTag("music:release_type", "original_release"),
                        _TypeMetaTag,
                        _NamespaceMetaTag
                    }
                }
            };

            [Theory]
            [MemberData(nameof(TestCases))]
            private void ShouldContainCorrectMetaTags(OpenGraphMusicSongTagHelper tagHelper, IEnumerable<MetaTag> expectedMetaTags)
            {
                var context = TagHelperTestUtils.CreateDefaultContext();
                var output = TagHelperTestUtils.CreateDefaultOutput();

                tagHelper.Process(context, output);

                output
                    .Should()
                    .HaveMetaTagsEquivalentTo(expectedMetaTags, options => options.WithStrictOrdering());
            }
        }

        public sealed class Type : OpenGraphMusicSongTagHelperTests
        {
            [Fact]
            private void ShouldBeMusicSong()
            {
                var tagHelper = new OpenGraphMusicSongTagHelper();

                string type = tagHelper.Type;

                type.Should().Be("music.song");
            }
        }
    }
}
