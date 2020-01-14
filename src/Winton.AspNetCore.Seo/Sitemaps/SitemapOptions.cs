// Copyright (c) Winton. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.Collections.Generic;

namespace Winton.AspNetCore.Seo.Sitemaps
{
    /// <summary>
    ///     The options used to create the sitemap.
    /// </summary>
    public sealed class SitemapOptions
    {
        /// <summary>
        ///     Gets or sets the URLs in the sitemap.
        /// </summary>
        public IEnumerable<SitemapUrlOptions>? Urls { get; set; }
    }
}