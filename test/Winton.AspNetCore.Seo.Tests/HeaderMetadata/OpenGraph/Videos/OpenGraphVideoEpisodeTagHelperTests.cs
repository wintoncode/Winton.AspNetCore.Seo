using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Winton.AspNetCore.Seo.HeaderMetadata.OpenGraph;
using Winton.AspNetCore.Seo.HeaderMetadata.OpenGraph.Videos;
using Xunit;

namespace Winton.AspNetCore.Seo.Tests.HeaderMetadata.OpenGraph.Videos
{
    public class OpenGraphVideoEpisodeTagHelperTests
    {
        public sealed class Process : OpenGraphVideoEpisodeTagHelperTests
        {
            private static readonly MetaTag _TypeMetaTag = new MetaTag("og:type", "video.episode");
            private static readonly MetaTag _NamespaceMetaTag = new MetaTag("OpenGraphNamespaceTagHelperComponent", "og: http://ogp.me/ns# video: http://ogp.me/ns/video#");

            public static IEnumerable<object[]> TestCases => new List<object[]>
            {
                new object[]
                {
                    new OpenGraphVideoEpisodeTagHelper(),
                    new List<MetaTag>
                    {
                        _TypeMetaTag,
                        _NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new OpenGraphVideoEpisodeTagHelper { Series = "http://example.com" },
                    new List<MetaTag>
                    {
                        new MetaTag("video:series", "http://example.com"),
                        _TypeMetaTag,
                        _NamespaceMetaTag
                    }
                }
            };

            [Theory]
            [MemberData(nameof(TestCases))]
            private void ShouldContainCorrectMetaTags(OpenGraphVideoEpisodeTagHelper tagHelper, IEnumerable<MetaTag> expectedMetaTags)
            {
                var context = TagHelperTestUtils.CreateDefaultContext();
                var output = TagHelperTestUtils.CreateDefaultOutput();

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

                string type = tagHelper.Type;

                type.Should().Be("video.episode");
            }
        }
    }
}
