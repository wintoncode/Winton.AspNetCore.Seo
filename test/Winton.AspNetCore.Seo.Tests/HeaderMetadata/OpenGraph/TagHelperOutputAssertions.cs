using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using FluentAssertions.Equivalency;
using FluentAssertions.Primitives;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Winton.AspNetCore.Seo.HeaderMetadata.OpenGraph
{
    internal sealed class TagHelperOutputAssertions :
        ReferenceTypeAssertions<TagHelperOutput, TagHelperOutputAssertions>
    {
        public TagHelperOutputAssertions(TagHelperOutput instance)
        {
            Subject = instance;
        }

        protected override string Identifier => nameof(OpenGraphTagHelper);

        public AndConstraint<TagHelperOutputAssertions> HaveMetaTagsEquivalentTo(
            IEnumerable<MetaTag> expectedMetaTags,
            string because = "",
            params object[] becauseArgs)
        {
            return HaveMetaTagsEquivalentTo(expectedMetaTags, options => options, because, becauseArgs);
        }

        public AndConstraint<TagHelperOutputAssertions> HaveMetaTagsEquivalentTo(
            IEnumerable<MetaTag> expectedMetaTags,
            Func<EquivalencyAssertionOptions<MetaTag>, EquivalencyAssertionOptions<MetaTag>> options,
            string because = "",
            params object[] becauseArgs)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(Subject.Content.GetContent());
            doc
                .DocumentNode
                .ChildNodes
                .Where(n => n.Name == "meta")
                .Select(
                    n => new MetaTag(
                        n.GetAttributeValue("property", string.Empty),
                        n.GetAttributeValue("content", string.Empty)))
                .Should().BeEquivalentTo(expectedMetaTags, options);

            return new AndConstraint<TagHelperOutputAssertions>(this);
        }
    }
}