using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Winton.AspNetCore.Seo.HeaderMetadata.OpenGraph;
using Xunit;

namespace Winton.AspNetCore.Seo.Tests.HeaderMetadata.OpenGraph
{
    public class OpenGraphProfileTagHelperTests
    {
        public sealed class Process : OpenGraphProfileTagHelperTests
        {
            private static readonly MetaTag _TypeMetaTag = new MetaTag("og:type", "profile");
            private static readonly MetaTag _NamespaceMetaTag = new MetaTag("OpenGraphNamespaceTagHelperComponent", "og: http://ogp.me/ns# profile: http://ogp.me/ns/profile#");

            public static IEnumerable<object[]> TestCases => new List<object[]>
            {
                new object[]
                {
                    new OpenGraphProfileTagHelper(),
                    new List<MetaTag>
                    {
                        _TypeMetaTag,
                        _NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new OpenGraphProfileTagHelper { FirstName = "Test" },
                    new List<MetaTag>
                    {
                        new MetaTag("profile:first_name", "Test"),
                        _TypeMetaTag,
                        _NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new OpenGraphProfileTagHelper { Gender = "Female" },
                    new List<MetaTag>
                    {
                        new MetaTag("profile:gender", "Female"),
                        _TypeMetaTag,
                        _NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new OpenGraphProfileTagHelper { LastName = "Test" },
                    new List<MetaTag>
                    {
                        new MetaTag("profile:last_name", "Test"),
                        _TypeMetaTag,
                        _NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new OpenGraphProfileTagHelper { Username = "a.test" },
                    new List<MetaTag>
                    {
                        new MetaTag("profile:username", "a.test"),
                        _TypeMetaTag,
                        _NamespaceMetaTag
                    }
                }
            };

            [Theory]
            [MemberData(nameof(TestCases))]
            private void ShouldContainCorrectMetaTags(OpenGraphProfileTagHelper tagHelper, IEnumerable<MetaTag> expectedMetaTags)
            {
                var context = TagHelperTestUtils.CreateDefaultContext();
                var output = TagHelperTestUtils.CreateDefaultOutput();

                tagHelper.Process(context, output);

                output
                    .Should()
                    .HaveMetaTagsEquivalentTo(expectedMetaTags, options => options.WithStrictOrdering());
            }
        }

        public sealed class Type : OpenGraphProfileTagHelperTests
        {
            [Fact]
            private void ShouldBeProfile()
            {
                var tagHelper = new OpenGraphProfileTagHelper();

                string type = tagHelper.Type;

                type.Should().Be("profile");
            }
        }
    }
}
