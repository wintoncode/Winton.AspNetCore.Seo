using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Winton.AspNetCore.Seo.HeaderMetadata.OpenGraph;
using Xunit;

namespace Winton.AspNetCore.Seo.Tests.HeaderMetadata.OpenGraph
{
    public class OpenGraphBookTagHelperTests
    {
        public sealed class Process : OpenGraphBookTagHelperTests
        {
            private static readonly MetaTag NamespaceMetaTag = new MetaTag(
                "OpenGraphNamespaceTagHelperComponent",
                "book: http://ogp.me/ns/book# og: http://ogp.me/ns#");

            private static readonly MetaTag TypeMetaTag = new MetaTag("og:type", "book");

            public static IEnumerable<object[]> TestCases => new List<object[]>
            {
                new object[]
                {
                    new OpenGraphBookTagHelper(),
                    new List<MetaTag>
                    {
                        TypeMetaTag,
                        NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new OpenGraphBookTagHelper { Authors = new List<string> { "https://example.com" } },
                    new List<MetaTag>
                    {
                        new MetaTag("book:author", "https://example.com"),
                        TypeMetaTag,
                        NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new OpenGraphBookTagHelper { Isbn = "1234123" },
                    new List<MetaTag>
                    {
                        new MetaTag("book:isbn", "1234123"),
                        TypeMetaTag,
                        NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new OpenGraphBookTagHelper { ReleaseDate = new DateTime(2017, 1, 1) },
                    new List<MetaTag>
                    {
                        new MetaTag("book:release_date", "2017-01-01T00:00:00.0000000"),
                        TypeMetaTag,
                        NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new OpenGraphBookTagHelper { Tags = new List<string> { "tag" } },
                    new List<MetaTag>
                    {
                        new MetaTag("book:tag", "tag"),
                        TypeMetaTag,
                        NamespaceMetaTag
                    }
                }
            };

            [Theory]
            [MemberData(nameof(TestCases))]
            private void ShouldContainCorrectMetaTags(
                OpenGraphBookTagHelper tagHelper,
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

        public sealed class Type : OpenGraphBookTagHelperTests
        {
            [Fact]
            private void ShouldBeBook()
            {
                var tagHelper = new OpenGraphBookTagHelper();

                string type = tagHelper.Type;

                type.Should().Be("book");
            }
        }
    }
}