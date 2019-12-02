using System.Collections.Generic;
using FluentAssertions;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Xunit;

namespace Winton.AspNetCore.Seo.HeaderMetadata.OpenGraph.Music
{
    public class OpenGraphMusicPlaylistTagHelperTests
    {
        public sealed class Process : OpenGraphMusicPlaylistTagHelperTests
        {
            private static readonly MetaTag NamespaceMetaTag = new MetaTag(
                "OpenGraphNamespaceTagHelperComponent",
                "music: http://ogp.me/ns/music# og: http://ogp.me/ns#");

            private static readonly MetaTag TypeMetaTag = new MetaTag("og:type", "music.playlist");

            public static IEnumerable<object[]> TestCases => new List<object[]>
            {
                new object[]
                {
                    new OpenGraphMusicPlaylistTagHelper(),
                    new List<MetaTag>
                    {
                        TypeMetaTag,
                        NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new OpenGraphMusicPlaylistTagHelper { Creator = "https://example.com" },
                    new List<MetaTag>
                    {
                        new MetaTag("music:creator", "https://example.com"),
                        TypeMetaTag,
                        NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new OpenGraphMusicPlaylistTagHelper
                    {
                        Songs = new List<Song> { new Song("http://example.com") }
                    },
                    new List<MetaTag>
                    {
                        new MetaTag("music:song_count", "1"),
                        new MetaTag("music:song", "http://example.com"),
                        TypeMetaTag,
                        NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new OpenGraphMusicPlaylistTagHelper
                    {
                        Songs = new List<Song> { new Song("http://example.com") { Disc = 2 } }
                    },
                    new List<MetaTag>
                    {
                        new MetaTag("music:song_count", "1"),
                        new MetaTag("music:song", "http://example.com"),
                        new MetaTag("music:song:disc", "2"),
                        TypeMetaTag,
                        NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new OpenGraphMusicPlaylistTagHelper
                    {
                        Songs = new List<Song> { new Song("http://example.com") { Track = 3 } }
                    },
                    new List<MetaTag>
                    {
                        new MetaTag("music:song_count", "1"),
                        new MetaTag("music:song", "http://example.com"),
                        new MetaTag("music:song:track", "3"),
                        TypeMetaTag,
                        NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new OpenGraphMusicPlaylistTagHelper
                    {
                        Songs =
                            new List<Song>
                            {
                                new Song("http://example.com/1.mp3"),
                                new Song("http://example.com/2.mp3")
                            }
                    },
                    new List<MetaTag>
                    {
                        new MetaTag("music:song_count", "2"),
                        new MetaTag("music:song", "http://example.com/1.mp3"),
                        new MetaTag("music:song", "http://example.com/2.mp3"),
                        TypeMetaTag,
                        NamespaceMetaTag
                    }
                }
            };

            [Theory]
            [MemberData(nameof(TestCases))]
            private void ShouldContainCorrectMetaTags(
                OpenGraphMusicPlaylistTagHelper tagHelper,
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

        public sealed class Type : OpenGraphMusicPlaylistTagHelperTests
        {
            [Fact]
            private void ShouldBeMusicPlaylist()
            {
                var tagHelper = new OpenGraphMusicPlaylistTagHelper();

                string? type = tagHelper.Type;

                type.Should().Be("music.playlist");
            }
        }
    }
}