using System;
using System.Collections.Generic;
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
            private static readonly MetaTag NamespaceMetaTag = new MetaTag(
                "OpenGraphNamespaceTagHelperComponent",
                "music: http://ogp.me/ns/music# og: http://ogp.me/ns#");

            private static readonly MetaTag TypeMetaTag = new MetaTag("og:type", "music.song");

            public static IEnumerable<object[]> TestCases => new List<object[]>
            {
                new object[]
                {
                    new OpenGraphMusicSongTagHelper(),
                    new List<MetaTag>
                    {
                        TypeMetaTag,
                        NamespaceMetaTag
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
                        TypeMetaTag,
                        NamespaceMetaTag
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
                        TypeMetaTag,
                        NamespaceMetaTag
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
                        TypeMetaTag,
                        NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new OpenGraphMusicSongTagHelper { Duration = 120 },
                    new List<MetaTag>
                    {
                        new MetaTag("music:duration", "120"),
                        TypeMetaTag,
                        NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new OpenGraphMusicSongTagHelper { Isrc = "abcde" },
                    new List<MetaTag>
                    {
                        new MetaTag("music:isrc", "abcde"),
                        TypeMetaTag,
                        NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new OpenGraphMusicSongTagHelper { Musicians = new List<string> { "https://example.com" } },
                    new List<MetaTag>
                    {
                        new MetaTag("music:musician", "https://example.com"),
                        TypeMetaTag,
                        NamespaceMetaTag
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
                        TypeMetaTag,
                        NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new OpenGraphMusicSongTagHelper { ReleaseDate = new DateTime(2017, 1, 1) },
                    new List<MetaTag>
                    {
                        new MetaTag("music:release_date", "2017-01-01T00:00:00.0000000"),
                        TypeMetaTag,
                        NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new OpenGraphMusicSongTagHelper { ReleaseType = "original_release" },
                    new List<MetaTag>
                    {
                        new MetaTag("music:release_type", "original_release"),
                        TypeMetaTag,
                        NamespaceMetaTag
                    }
                }
            };

            [Theory]
            [MemberData(nameof(TestCases))]
            private void ShouldContainCorrectMetaTags(
                OpenGraphMusicSongTagHelper tagHelper,
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