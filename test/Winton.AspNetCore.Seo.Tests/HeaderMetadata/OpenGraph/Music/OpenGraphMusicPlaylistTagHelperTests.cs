using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Winton.AspNetCore.Seo.HeaderMetadata.OpenGraph;
using Winton.AspNetCore.Seo.HeaderMetadata.OpenGraph.Music;
using Xunit;

namespace Winton.AspNetCore.Seo.Tests.HeaderMetadata.OpenGraph.Music
{
    public class OpenGraphMusicPlaylistTagHelperTests
    {
        public sealed class Process : OpenGraphMusicPlaylistTagHelperTests
        {
            private static readonly MetaTag _TypeMetaTag = new MetaTag("og:type", "music.playlist");
            private static readonly MetaTag _NamespaceMetaTag = new MetaTag("OpenGraphNamespaceTagHelperComponent", "music: http://ogp.me/ns/music# og: http://ogp.me/ns#");

            public static IEnumerable<object[]> TestCases => new List<object[]>
            {
                new object[]
                {
                    new OpenGraphMusicPlaylistTagHelper(),
                    new List<MetaTag>
                    {
                        _TypeMetaTag,
                        _NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new OpenGraphMusicPlaylistTagHelper { Creator = "https://example.com" },
                    new List<MetaTag>
                    {
                        new MetaTag("music:creator", "https://example.com"),
                        _TypeMetaTag,
                        _NamespaceMetaTag
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
                        new MetaTag("music:song", "http://example.com"),
                        new MetaTag("music:song_count", "1"),
                        _TypeMetaTag,
                        _NamespaceMetaTag
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
                        new MetaTag("music:song", "http://example.com"),
                        new MetaTag("music:song:disc", "2"),
                        new MetaTag("music:song_count", "1"),
                        _TypeMetaTag,
                        _NamespaceMetaTag
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
                        new MetaTag("music:song", "http://example.com"),
                        new MetaTag("music:song:track", "3"),
                        new MetaTag("music:song_count", "1"),
                        _TypeMetaTag,
                        _NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new OpenGraphMusicPlaylistTagHelper { Songs = new List<Song> { new Song("http://example.com/1.mp3"), new Song("http://example.com/2.mp3") } },
                    new List<MetaTag>
                    {
                        new MetaTag("music:song", "http://example.com/1.mp3"),
                        new MetaTag("music:song", "http://example.com/2.mp3"),
                        new MetaTag("music:song_count", "2"),
                        _TypeMetaTag,
                        _NamespaceMetaTag
                    }
                }
            };

            [Theory]
            [MemberData(nameof(TestCases))]
            private void ShouldContainCorrectMetaTags(OpenGraphMusicPlaylistTagHelper tagHelper, IEnumerable<MetaTag> expectedMetaTags)
            {
                var context = TagHelperTestUtils.CreateDefaultContext();
                var output = TagHelperTestUtils.CreateDefaultOutput();

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

                string type = tagHelper.Type;

                type.Should().Be("music.playlist");
            }
        }
    }
}
