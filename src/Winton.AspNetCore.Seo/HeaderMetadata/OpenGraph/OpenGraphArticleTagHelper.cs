// Copyright (c) Winton. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;

namespace Winton.AspNetCore.Seo.HeaderMetadata.OpenGraph
{
    /// <summary>
    ///     An Open Graph tag helper for article type pages.
    /// </summary>
    [OpenGraphNamespaceAttribute("article", "http://ogp.me/ns/article#")]
    public sealed class OpenGraphArticleTagHelper : OpenGraphTagHelper
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="OpenGraphArticleTagHelper"/> class.
        /// </summary>
        public OpenGraphArticleTagHelper()
            : base("article")
        {
        }

        /// <summary>
        ///     Gets or sets the authors of this article. Each value is a URL of a page with og:type profile.
        /// </summary>
        [OpenGraphProperty(Name = "author")]
        public IEnumerable<string> Authors { get; set; }

        /// <summary>
        ///     Gets or sets the content tier that the publisher sets to specify whether article is free, locked, or metered.
        /// </summary>
        public string ContentTier { get; set; }

        /// <summary>
        ///     Gets or sets the expiration time of the article.
        /// </summary>
        public DateTime? ExpirationTime { get; set; }

        /// <summary>
        ///     Gets or sets the time that the article was last modified.
        /// </summary>
        public DateTime? ModifiedTime { get; set; }

        /// <summary>
        ///     Gets or sets the time that the article was published.
        /// </summary>
        public DateTime? PublishedTime { get; set; }

        /// <summary>
        ///     Gets or sets a reference to the publisher. A URL of a page with og:type profile.
        /// </summary>
        public string Publisher { get; set; }

        /// <summary>
        ///     Gets or sets the high-level section name. E.g. Technology.
        /// </summary>
        public string Section { get; set; }

        /// <summary>
        ///     Gets or sets the tag words associated with this article.
        /// </summary>
        [OpenGraphProperty(Name = "tag")]
        public IEnumerable<string> Tags { get; set; }
    }
}