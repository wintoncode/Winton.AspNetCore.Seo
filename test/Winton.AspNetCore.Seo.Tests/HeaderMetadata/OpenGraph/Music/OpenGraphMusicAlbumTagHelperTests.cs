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
    public class OpenGraphMusicAlbumTagHelperTests
    {
        public sealed class Process : OpenGraphMusicAlbumTagHelperTests
        {
            private static readonly MetaTag _TypeMetaTag = new MetaTag("og:type", "music.album");
            private static readonly MetaTag _NamespaceMetaTag = new MetaTag("OpenGraphNamespaceTagHelperComponent", "music: http://ogp.me/ns/music# og: http://ogp.me/ns#");

            public static IEnumerable<object[]> TestCases => new List<object[]>
            {
                new object[]
                {
                    new OpenGraphMusicAlbumTagHelper(),
                    new List<MetaTag>
                    {
                        _TypeMetaTag,
                        _NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new OpenGraphMusicAlbumTagHelper { Musicians = new List<string> { "https://example.com" } },
                    new List<MetaTag>
                    {
                        new MetaTag("music:musician", "https://example.com"),
                        _TypeMetaTag,
                        _NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new OpenGraphMusicAlbumTagHelper { ReleaseDate = new DateTime(2020, 1, 1) },
                    new List<MetaTag>
                    {
                        new MetaTag("music:release_date", "2020-01-01T00:00:00.0000000"),
                        _TypeMetaTag,
                        _NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new OpenGraphMusicAlbumTagHelper { ReleaseType = "anthology" },
                    new List<MetaTag>
                    {
                        new MetaTag("music:release_type", "anthology"),
                        _TypeMetaTag,
                        _NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new OpenGraphMusicAlbumTagHelper
                    {
                        Songs = new List<Song> { new Song("http://example.com") }
                    },
                    new List<MetaTag>
                    {
                        new MetaTag("music:song", "http://example.com"),
                        _TypeMetaTag,
                        _NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new OpenGraphMusicAlbumTagHelper
                    {
                        Songs = new List<Song> { new Song("https://example.com") { Disc = 1 } }
                    },
                    new List<MetaTag>
                    {
                        new MetaTag("music:song", "https://example.com"),
                        new MetaTag("music:song:disc", "1"),
                        _TypeMetaTag,
                        _NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new OpenGraphMusicAlbumTagHelper
                    {
                        Songs = new List<Song> { new Song("https://example.com") { Track = 3 } }
                    },
                    new List<MetaTag>
                    {
                        new MetaTag("music:song", "https://example.com"),
                        new MetaTag("music:song:track", "3"),
                        _TypeMetaTag,
                        _NamespaceMetaTag
                    }
                }
            };

            [Theory]
            [MemberData(nameof(TestCases))]
            private void ShouldContainCorrectMetaTags(OpenGraphMusicAlbumTagHelper tagHelper, IEnumerable<MetaTag> expectedMetaTags)
            {
                var context = TagHelperTestUtils.CreateDefaultContext();
                var output = TagHelperTestUtils.CreateDefaultOutput();

                tagHelper.Process(context, output);

                output
                    .Should()
                    .HaveMetaTagsEquivalentTo(expectedMetaTags, options => options.WithStrictOrdering());
            }
        }

        public sealed class Type : OpenGraphMusicAlbumTagHelperTests
        {
            [Fact]
            private void ShouldBeMusicAlbum()
            {
                var tagHelper = new OpenGraphMusicAlbumTagHelper();

                string type = tagHelper.Type;

                type.Should().Be("music.album");
            }
        }
    }
}
