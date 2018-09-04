using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Xunit;

namespace Winton.AspNetCore.Seo.HeaderMetadata.OpenGraph
{
    public class OpenGraphArticleTagHelperTests
    {
        public sealed class Process : OpenGraphArticleTagHelperTests
        {
            private static readonly MetaTag NamespaceMetaTag = new MetaTag(
                "OpenGraphNamespaceTagHelperComponent",
                "article: http://ogp.me/ns/article# og: http://ogp.me/ns#");

            private static readonly MetaTag TypeMetaTag = new MetaTag("og:type", "article");

            public static IEnumerable<object[]> TestCases => new List<object[]>
            {
                new object[]
                {
                    new OpenGraphArticleTagHelper(),
                    new List<MetaTag>
                    {
                        TypeMetaTag,
                        NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new OpenGraphArticleTagHelper { Authors = new List<string> { "https://example.com" } },
                    new List<MetaTag>
                    {
                        new MetaTag("article:author", "https://example.com"),
                        TypeMetaTag,
                        NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new OpenGraphArticleTagHelper { ContentTier = "locked" },
                    new List<MetaTag>
                    {
                        new MetaTag("article:content_tier", "locked"),
                        TypeMetaTag,
                        NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new OpenGraphArticleTagHelper { ExpirationTime = new DateTime(2020, 1, 1) },
                    new List<MetaTag>
                    {
                        new MetaTag("article:expiration_time", "2020-01-01T00:00:00.0000000"),
                        TypeMetaTag,
                        NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new OpenGraphArticleTagHelper { ModifiedTime = new DateTime(2017, 1, 1) },
                    new List<MetaTag>
                    {
                        new MetaTag("article:modified_time", "2017-01-01T00:00:00.0000000"),
                        TypeMetaTag,
                        NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new OpenGraphArticleTagHelper
                    {
                        PublishedTime = new DateTime(2016, 1, 1, 10, 5, 36, 123, DateTimeKind.Utc)
                    },
                    new List<MetaTag>
                    {
                        new MetaTag("article:published_time", "2016-01-01T10:05:36.1230000Z"),
                        TypeMetaTag,
                        NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new OpenGraphArticleTagHelper { Publisher = "http://example.com" },
                    new List<MetaTag>
                    {
                        new MetaTag("article:publisher", "http://example.com"),
                        TypeMetaTag,
                        NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new OpenGraphArticleTagHelper { Section = "Technology" },
                    new List<MetaTag>
                    {
                        new MetaTag("article:section", "Technology"),
                        TypeMetaTag,
                        NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new OpenGraphArticleTagHelper { Tags = new List<string> { "tag" } },
                    new List<MetaTag>
                    {
                        new MetaTag("article:tag", "tag"),
                        TypeMetaTag,
                        NamespaceMetaTag
                    }
                }
            };

            [Theory]
            [MemberData(nameof(TestCases))]
            private void ShouldContainCorrectMetaTags(
                OpenGraphArticleTagHelper tagHelper,
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

        public sealed class Type : OpenGraphArticleTagHelperTests
        {
            [Fact]
            private void ShouldBeArticle()
            {
                var tagHelper = new OpenGraphArticleTagHelper();

                string type = tagHelper.Type;

                type.Should().Be("article");
            }
        }
    }
}