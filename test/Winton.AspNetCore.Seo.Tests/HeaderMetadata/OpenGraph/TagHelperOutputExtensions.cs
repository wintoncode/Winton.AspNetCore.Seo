using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Winton.AspNetCore.Seo.Tests.HeaderMetadata.OpenGraph
{
    internal static class TagHelperOutputExtensions
    {
        internal static TagHelperOutputAssertions Should(this TagHelperOutput instance)
        {
            return new TagHelperOutputAssertions(instance);
        }
    }
}