using System.Collections.Generic;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Winton.AspNetCore.Seo.HeaderMetadata.OpenGraph;
using Xunit;

namespace Winton.AspNetCore.Seo.Tests.HeaderMetadata.OpenGraph
{
    public class OpenGraphTagHelperTests
    {
        public sealed class Process : OpenGraphTagHelperTests
        {
            private static readonly MetaTag NamespaceMetaTag = new MetaTag(
                "OpenGraphNamespaceTagHelperComponent",
                "og: http://ogp.me/ns#");

            private static readonly MetaTag TypeMetaTag = new MetaTag("og:type", "test");

            public static IEnumerable<object[]> TestCases => new List<object[]>
            {
                new object[]
                {
                    new TestOpenGraphTagHelper(),
                    new List<MetaTag>
                    {
                        TypeMetaTag,
                        NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new TestOpenGraphTagHelper { Audio = new Audio("http://example.com") },
                    new List<MetaTag>
                    {
                        new MetaTag("og:audio", "http://example.com"),
                        TypeMetaTag,
                        NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new TestOpenGraphTagHelper
                    {
                        Audio = new Audio("http://example.com") { SecureUrl = "https://example.com" }
                    },
                    new List<MetaTag>
                    {
                        new MetaTag("og:audio", "http://example.com"),
                        new MetaTag("og:audio:secure_url", "https://example.com"),
                        TypeMetaTag,
                        NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new TestOpenGraphTagHelper { Description = "Test description" },
                    new List<MetaTag>
                    {
                        new MetaTag("og:description", "Test description"),
                        TypeMetaTag,
                        NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new TestOpenGraphTagHelper { Determiner = "a" },
                    new List<MetaTag>
                    {
                        new MetaTag("og:determiner", "a"),
                        TypeMetaTag,
                        NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new TestOpenGraphTagHelper
                    {
                        Images = new List<Image> { new Image("http://example.com") }
                    },
                    new List<MetaTag>
                    {
                        new MetaTag("og:image", "http://example.com"),
                        TypeMetaTag,
                        NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new TestOpenGraphTagHelper
                    {
                        Images = new List<Image> { new Image("http://example.com") { Alt = "Alt text" } }
                    },
                    new List<MetaTag>
                    {
                        new MetaTag("og:image", "http://example.com"),
                        new MetaTag("og:image:alt", "Alt text"),
                        TypeMetaTag,
                        NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new TestOpenGraphTagHelper
                    {
                        Images = new List<Image> { new Image("http://example.com") { Height = 400 } }
                    },
                    new List<MetaTag>
                    {
                        new MetaTag("og:image", "http://example.com"),
                        new MetaTag("og:image:height", "400"),
                        TypeMetaTag,
                        NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new TestOpenGraphTagHelper
                    {
                        Images =
                            new List<Image> { new Image("http://example.com") { SecureUrl = "https://example.com" } }
                    },
                    new List<MetaTag>
                    {
                        new MetaTag("og:image", "http://example.com"),
                        new MetaTag("og:image:secure_url", "https://example.com"),
                        TypeMetaTag,
                        NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new TestOpenGraphTagHelper
                    {
                        Images = new List<Image> { new Image("http://example.com") { Width = 500 } }
                    },
                    new List<MetaTag>
                    {
                        new MetaTag("og:image", "http://example.com"),
                        new MetaTag("og:image:width", "500"),
                        TypeMetaTag,
                        NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new TestOpenGraphTagHelper { Locale = new Locale("en_GB") },
                    new List<MetaTag>
                    {
                        new MetaTag("og:locale", "en_GB"),
                        TypeMetaTag,
                        NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new TestOpenGraphTagHelper
                    {
                        Locale = new Locale("en_GB") { Alternate = new List<string> { "en_US" } }
                    },
                    new List<MetaTag>
                    {
                        new MetaTag("og:locale", "en_GB"),
                        new MetaTag("og:locale:alternate", "en_US"),
                        TypeMetaTag,
                        NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new TestOpenGraphTagHelper { SiteName = "My Website" },
                    new List<MetaTag>
                    {
                        new MetaTag("og:site_name", "My Website"),
                        TypeMetaTag,
                        NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new TestOpenGraphTagHelper { Title = "My Website" },
                    new List<MetaTag>
                    {
                        new MetaTag("og:title", "My Website"),
                        TypeMetaTag,
                        NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new TestOpenGraphTagHelper { Url = "http://example.com" },
                    new List<MetaTag>
                    {
                        TypeMetaTag,
                        new MetaTag("og:url", "http://example.com"),
                        NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new TestOpenGraphTagHelper { Video = new Video("http://example.com") },
                    new List<MetaTag>
                    {
                        TypeMetaTag,
                        new MetaTag("og:video", "http://example.com"),
                        NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new TestOpenGraphTagHelper { Video = new Video("http://example.com") { Height = 10 } },
                    new List<MetaTag>
                    {
                        TypeMetaTag,
                        new MetaTag("og:video", "http://example.com"),
                        new MetaTag("og:video:height", "10"),
                        NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new TestOpenGraphTagHelper
                    {
                        Video = new Video("http://example.com") { SecureUrl = "https://example.com" }
                    },
                    new List<MetaTag>
                    {
                        TypeMetaTag,
                        new MetaTag("og:video", "http://example.com"),
                        new MetaTag("og:video:secure_url", "https://example.com"),
                        NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new TestOpenGraphTagHelper { Video = new Video("http://example.com") { Width = 100 } },
                    new List<MetaTag>
                    {
                        TypeMetaTag,
                        new MetaTag("og:video", "http://example.com"),
                        new MetaTag("og:video:width", "100"),
                        NamespaceMetaTag
                    }
                },
                new object[]
                {
                    new TestOpenGraphTagHelper
                    {
                        Audio = new Audio("http://example.com") { SecureUrl = "https://example.com" },
                        Description = "Test description",
                        Images = new List<Image>
                        {
                            new Image("http://example.com/1.jpg") { Height = 200, Width = 150 },
                            new Image("http://example.com/2.jpg") { Height = 100, Width = 50 }
                        }
                    },
                    new List<MetaTag>
                    {
                        new MetaTag("og:audio", "http://example.com"),
                        new MetaTag("og:audio:secure_url", "https://example.com"),
                        new MetaTag("og:description", "Test description"),
                        new MetaTag("og:image", "http://example.com/1.jpg"),
                        new MetaTag("og:image:height", "200"),
                        new MetaTag("og:image:width", "150"),
                        new MetaTag("og:image", "http://example.com/2.jpg"),
                        new MetaTag("og:image:height", "100"),
                        new MetaTag("og:image:width", "50"),
                        TypeMetaTag,
                        NamespaceMetaTag
                    }
                }
            };

            [Theory]
            [MemberData(nameof(TestCases))]
            private void ShouldContainCorrectMetaTags(
                TestOpenGraphTagHelper tagHelper,
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

        private sealed class TestOpenGraphTagHelper : OpenGraphTagHelper
        {
            public TestOpenGraphTagHelper()
                : base("test")
            {
            }
        }
    }
}