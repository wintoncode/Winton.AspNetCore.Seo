// Copyright (c) Winton. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Winton.AspNetCore.Seo.HeaderMetadata.OpenGraph
{
    internal sealed class OpenGraphNamespaceTagHelperComponent : TagHelperComponent
    {
        public override int Order => 1;

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (context.TagName.Equals("head", StringComparison.OrdinalIgnoreCase))
            {
                TagHelperContent tagHelperContent = await output.GetChildContentAsync();
                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(tagHelperContent.GetContent());
                HtmlNode namespaceMetaTag = htmlDoc
                    .DocumentNode
                    .Descendants("meta")
                    .SingleOrDefault(node => node.Attributes.Any(HasNamespaceProperty));
                if (namespaceMetaTag != null)
                {
                    output.Attributes.Add(
                        "prefix",
                        namespaceMetaTag.Attributes.SingleOrDefault(attr => attr.Name == "content")?.Value);
                    namespaceMetaTag.Remove();
                    output.Content.SetHtmlContent(htmlDoc.DocumentNode.WriteTo());
                }
            }
        }

        private bool HasNamespaceProperty(HtmlAttribute attribute)
        {
            return attribute.Name == "property" &&
                   attribute.Value == nameof(OpenGraphNamespaceTagHelperComponent);
        }
    }
}