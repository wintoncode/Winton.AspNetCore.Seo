// Copyright (c) Winton. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Winton.AspNetCore.Seo.Robots;
using Winton.AspNetCore.Seo.Sitemaps;

namespace Winton.AspNetCore.Seo
{
    /// <summary>
    ///     The options used to configure the SEO middleware.
    /// </summary>
    public sealed class SeoOptions
    {
        /// <summary>
        ///     Gets the Robots.txt options.
        /// </summary>
        public RobotsTxtOptions RobotsTxt { get; } = new RobotsTxtOptions();

        /// <summary>
        ///     Gets the Sitemap.xml options.
        /// </summary>
        public SitemapOptions Sitemap { get; } = new SitemapOptions();
    }
}