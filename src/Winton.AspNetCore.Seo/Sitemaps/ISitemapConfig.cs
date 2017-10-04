// Copyright (c) Winton. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.Collections.Generic;

namespace Winton.AspNetCore.Seo.Sitemaps
{
    /// <summary>
    ///     The config used to define the sitemap.
    /// </summary>
    public interface ISitemapConfig
    {
        /// <summary>
        ///     Gets a list of <see cref="SitemapConfigUrl" /> values.
        /// </summary>
        List<SitemapConfigUrl> Urls { get; }
    }
}