// Copyright (c) Winton. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Winton.AspNetCore.Seo.HeaderMetadata.OpenGraph
{
    /// <summary>
    ///     A tag helper that makes it easy to add <a href="http://ogp.me/">Open Graph</a> meta tags to a page.
    ///     These values determine how the page appears when shared on social networks that support Open Graph.
    /// </summary>
    [OpenGraphNamespaceAttribute("og", "http://ogp.me/ns#")]
    public abstract class OpenGraphTagHelper : TagHelper
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="OpenGraphTagHelper"/> class.
        /// </summary>
        /// <param name="type">The type of Open Graph object.</param>
        protected OpenGraphTagHelper(string type)
        {
            Type = type;
        }

        /// <summary>
        ///     Gets or sets the audio to accompany this object.
        /// </summary>
        public Audio Audio { get; set; }

        /// <summary>
        ///     Gets or sets a one to two sentence description of this object.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     Gets or sets the word that appears before this object's title in a sentence.
        ///     An enum of (a, an, the, "", auto).
        /// </summary>
        public string Determiner { get; set; }

        /// <summary>
        ///     Gets or sets the images which should represent this object within the graph.
        /// </summary>
        [OpenGraphProperty(Name = "image")]
        public IEnumerable<Image> Images { get; set; }

        /// <summary>
        ///     Gets or sets the locale these tags are marked up in.
        /// </summary>
        public Locale Locale { get; set; }

        /// <summary>
        ///     Gets or sets the name which should be displayed for the overall site that this object is part of.
        /// </summary>
        public string SiteName { get; set; }

        /// <summary>
        ///     Gets or sets the title of this object as it should appear within the graph.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     Gets the type of this open graph object.
        /// </summary>
        public string Type { get; }

        /// <summary>
        ///      Gets or sets the canonical URL of the object that will be used as its permanent ID in the graph.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        ///      Gets or sets a video that complements this object.
        /// </summary>
        public Video Video { get; set; }

        /// <inheritdoc />
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = null;
            foreach (MetaTag metaTag in GetMetaTags().Where(mt => !string.IsNullOrWhiteSpace(mt.Value)))
            {
                output.Content.AppendHtml(metaTag.ToString());
            }
        }

        private IEnumerable<MetaTag> GetMetaTags()
        {
            IEnumerable<PropertyInfo> openGraphProperties = this
                .GetType()
                .GetProperties()
                .Where(p => typeof(OpenGraphTagHelper).IsAssignableFrom(p.DeclaringType));

            return openGraphProperties
                .SelectMany(p => p.GetOpenGraphPropertyInfo(this).ToMetaTags())
                .Concat(
                    new List<MetaTag>
                    {
                        CreateTemporaryNamepsaceMetaTag(openGraphProperties)
                    });
        }

        private MetaTag CreateTemporaryNamepsaceMetaTag(IEnumerable<PropertyInfo> openGraphProperties)
        {
            return new MetaTag(
                nameof(OpenGraphNamespaceTagHelperComponent),
                string.Join(
                    " ",
                    openGraphProperties.Select(p => p.GetOpenGraphNamespace().ToPrefixValue()).Distinct().OrderBy(s => s)));
        }
    }
}