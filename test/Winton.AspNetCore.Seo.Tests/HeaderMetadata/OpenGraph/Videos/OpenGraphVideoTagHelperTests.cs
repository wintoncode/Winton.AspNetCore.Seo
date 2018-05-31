using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Xunit;

namespace Winton.AspNetCore.Seo.HeaderMetadata.OpenGraph.Videos
{
    public class OpenGraphVideoTagHelperTests
    {
        public sealed class Process : OpenGraphVideoTagHelperTests
        {
            private static readonly MetaTag NamespaceMetaTag = new MetaTag(
                "OpenGraphNamespaceTagHelperComponent",
                "og: http://ogp.me/ns# video: http://ogp.me/ns/video#");

            private static readonly MetaTag TypeMetaTag = new MetaTag("og:type", "video.test");

            public static IEnumerable<object[]> TestCases => new List<object[]>
            {
                new object[]
                {
                    new TestOpenGraphVideoTagHelper(),
                    new List<MetaTag>
                    {
                        TypeMetaTag,
                        NamespaceMetaTag
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
                        TypeMetaTag,
                        NamespaceMetaTag
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
                        TypeMetaTag,
                        NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new TestOpenGraphVideoTagHelper { Directors = new List<string> { "http://example.com" } },
                    new List<MetaTag>
                    {
                        new MetaTag("video:director", "http://example.com"),
                        TypeMetaTag,
                        NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new TestOpenGraphVideoTagHelper { Duration = 1234 },
                    new List<MetaTag>
                    {
                        new MetaTag("video:duration", "1234"),
                        TypeMetaTag,
                        NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new TestOpenGraphVideoTagHelper { ReleaseDate = new DateTime(2017, 1, 1) },
                    new List<MetaTag>
                    {
                        new MetaTag("video:release_date", "2017-01-01T00:00:00.0000000"),
                        TypeMetaTag,
                        NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new TestOpenGraphVideoTagHelper { Tags = new List<string> { "test" } },
                    new List<MetaTag>
                    {
                        new MetaTag("video:tag", "test"),
                        TypeMetaTag,
                        NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new TestOpenGraphVideoTagHelper { Writers = new List<string> { "http://example.com" } },
                    new List<MetaTag>
                    {
                        new MetaTag("video:writer", "http://example.com"),
                        TypeMetaTag,
                        NamespaceMetaTag
                    }
                }
            };

            [Theory]
            [MemberData(nameof(TestCases))]
            private void ShouldContainCorrectMetaTags(
                TestOpenGraphVideoTagHelper tagHelper,
                IEnumerable<MetaTag> expectedMetaTags)
            {
                TagHelperContext context = TagHelperTestUtils.CreateDefaultContext();
                TagHelperOutput output = TagHelperTestUtils.CreateDefaultOutput();

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