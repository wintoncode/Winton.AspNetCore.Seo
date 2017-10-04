// Copyright (c) Winton. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Winton.AspNetCore.Seo.Sitemaps
{
    /// <summary>
    ///     Class that defines the data in the a sitemap. Has the <see cref="CollectionDataContractAttribute" /> for
    ///     serialisation.
    /// </summary>
    [CollectionDataContract(Name = "urlset", Namespace = "http://www.sitemaps.org/schemas/sitemap/0.9")]
    public sealed class Sitemap : List<SitemapUrl>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Sitemap" /> class.
        ///     Default constructor used in serialisation.
        /// </summary>
        public Sitemap()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Sitemap" /> class from an <see cref="IEnumerable{SitemapUrl}" />.
        /// </summary>
        /// <param name="sitemapUrls">The URLs to create the sitemap from.</param>
        public Sitemap(IEnumerable<SitemapUrl> sitemapUrls)
            : base(sitemapUrls)
        {
        }
    }
}