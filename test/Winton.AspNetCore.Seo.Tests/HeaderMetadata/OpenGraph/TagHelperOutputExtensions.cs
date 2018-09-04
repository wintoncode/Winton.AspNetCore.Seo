using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Winton.AspNetCore.Seo.HeaderMetadata.OpenGraph
{
    internal static class TagHelperOutputExtensions
    {
        internal static TagHelperOutputAssertions Should(this TagHelperOutput instance)
        {
            return new TagHelperOutputAssertions(instance);
        }
    }
}