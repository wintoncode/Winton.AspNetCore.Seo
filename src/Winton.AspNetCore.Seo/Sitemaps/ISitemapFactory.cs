// Copyright (c) Winton. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

namespace Winton.AspNetCore.Seo.Sitemaps
{
    /// <summary>
    ///     A factory for creating a <see cref="Sitemap" />.
    /// </summary>
    public interface ISitemapFactory
    {
        /// <summary>
        ///     Creates a <see cref="Sitemap" />.
        /// </summary>
        /// <returns>The <see cref="Sitemap" />.</returns>
        Sitemap Create();
    }
}