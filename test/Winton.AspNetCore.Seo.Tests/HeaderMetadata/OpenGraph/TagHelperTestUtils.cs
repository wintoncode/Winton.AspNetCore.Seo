using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Winton.AspNetCore.Seo.Tests.HeaderMetadata.OpenGraph
{
    public sealed class TagHelperTestUtils
    {
        public static TagHelperContext CreateDefaultContext()
        {
            return new TagHelperContext(
                new TagHelperAttributeList(),
                new Dictionary<object, object>(),
                string.Empty);
        }

        public static TagHelperOutput CreateDefaultOutput()
        {
            return new TagHelperOutput(
                string.Empty,
                new TagHelperAttributeList(),
                (useCachedResult, encoder) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent()));
        }
    }
}