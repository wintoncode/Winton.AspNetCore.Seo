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
    public class OpenGraphVideoTagHelperTests
    {
        public sealed class Process : OpenGraphVideoTagHelperTests
        {
            private static readonly MetaTag _TypeMetaTag = new MetaTag("og:type", "video.test");
            private static readonly MetaTag _NamespaceMetaTag = new MetaTag("OpenGraphNamespaceTagHelperComponent", "og: http://ogp.me/ns# video: http://ogp.me/ns/video#");

            public static IEnumerable<object[]> TestCases => new List<object[]>
            {
                new object[]
                {
                    new TestOpenGraphVideoTagHelper(),
                    new List<MetaTag>
                    {
                        _TypeMetaTag,
                        _NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new TestOpenGraphVideoTagHelper
                    {
                        Actors = new List<Actor> { new Actor("http://example.com") }
                    },
                    new List<MetaTag>
                    {
                        new MetaTag("video:actor", "http://example.com"),
                        _TypeMetaTag,
                        _NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new TestOpenGraphVideoTagHelper
                    {
                        Actors = new List<Actor> { new Actor("http://example.com") { Role = "Hero" } }
                    },
                    new List<MetaTag>
                    {
                        new MetaTag("video:actor", "http://example.com"),
                        new MetaTag("video:actor:role", "Hero"),
                        _TypeMetaTag,
                        _NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new TestOpenGraphVideoTagHelper { Directors = new List<string> { "http://example.com" } },
                    new List<MetaTag>
                    {
                        new MetaTag("video:director", "http://example.com"),
                        _TypeMetaTag,
                        _NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new TestOpenGraphVideoTagHelper { Duration = 1234 },
                    new List<MetaTag>
                    {
                        new MetaTag("video:duration", "1234"),
                        _TypeMetaTag,
                        _NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new TestOpenGraphVideoTagHelper { ReleaseDate = new DateTime(2017, 1, 1) },
                    new List<MetaTag>
                    {
                        new MetaTag("video:release_date", "2017-01-01T00:00:00.0000000"),
                        _TypeMetaTag,
                        _NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new TestOpenGraphVideoTagHelper { Tags = new List<string> { "test" } },
                    new List<MetaTag>
                    {
                        new MetaTag("video:tag", "test"),
                        _TypeMetaTag,
                        _NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new TestOpenGraphVideoTagHelper { Writers = new List<string> { "http://example.com" } },
                    new List<MetaTag>
                    {
                        new MetaTag("video:writer", "http://example.com"),
                        _TypeMetaTag,
                        _NamespaceMetaTag
                    }
                }
            };

            [Theory]
            [MemberData(nameof(TestCases))]
            private void ShouldContainCorrectMetaTags(TestOpenGraphVideoTagHelper tagHelper, IEnumerable<MetaTag> expectedMetaTags)
            {
                var context = TagHelperTestUtils.CreateDefaultContext();
                var output = TagHelperTestUtils.CreateDefaultOutput();

                tagHelper.Process(context, output);

                output
                    .Should()
                    .HaveMetaTagsEquivalentTo(expectedMetaTags, options => options.WithStrictOrdering());
            }

            private sealed class TestOpenGraphVideoTagHelper : OpenGraphVideoTagHelper
            {
                public TestOpenGraphVideoTagHelper()
                    : base("test")
                {
                }
            }
        }
    }
}
