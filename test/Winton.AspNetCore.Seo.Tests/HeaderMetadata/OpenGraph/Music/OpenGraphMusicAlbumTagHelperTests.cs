using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Xunit;

namespace Winton.AspNetCore.Seo.HeaderMetadata.OpenGraph.Music
{
    public class OpenGraphMusicAlbumTagHelperTests
    {
        public sealed class Process : OpenGraphMusicAlbumTagHelperTests
        {
            private static readonly MetaTag NamespaceMetaTag = new MetaTag(
                "OpenGraphNamespaceTagHelperComponent",
                "music: http://ogp.me/ns/music# og: http://ogp.me/ns#");

            private static readonly MetaTag TypeMetaTag = new MetaTag("og:type", "music.album");

            public static IEnumerable<object[]> TestCases => new List<object[]>
            {
                new object[]
                {
                    new OpenGraphMusicAlbumTagHelper(),
                    new List<MetaTag>
                    {
                        TypeMetaTag,
                        NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new OpenGraphMusicAlbumTagHelper { Musicians = new List<string> { "https://example.com" } },
                    new List<MetaTag>
                    {
                        new MetaTag("music:musician", "https://example.com"),
                        TypeMetaTag,
                        NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new OpenGraphMusicAlbumTagHelper { ReleaseDate = new DateTime(2020, 1, 1) },
                    new List<MetaTag>
                    {
                        new MetaTag("music:release_date", "2020-01-01T00:00:00.0000000"),
                        TypeMetaTag,
                        NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new OpenGraphMusicAlbumTagHelper { ReleaseType = "anthology" },
                    new List<MetaTag>
                    {
                        new MetaTag("music:release_type", "anthology"),
                        TypeMetaTag,
                        NamespaceMetaTag
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
                        TypeMetaTag,
                        NamespaceMetaTag
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
                        TypeMetaTag,
                        NamespaceMetaTag
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
                        TypeMetaTag,
                        NamespaceMetaTag
                    }
                }
            };

            [Theory]
            [MemberData(nameof(TestCases))]
            private void ShouldContainCorrectMetaTags(
                OpenGraphMusicAlbumTagHelper tagHelper,
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