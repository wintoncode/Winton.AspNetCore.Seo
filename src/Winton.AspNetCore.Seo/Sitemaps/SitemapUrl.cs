// Copyright (c) Winton. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.Runtime.Serialization;

namespace Winton.AspNetCore.Seo.Sitemaps
{
    /// <summary>
    ///     This class represents a single URL in a sitemap.
    ///     See the <a href="https://www.sitemaps.org">sitemaps.org</a> site for more information about this format.
    /// </summary>
    [DataContract(Name = "url", Namespace = "http://www.sitemaps.org/schemas/sitemap/0.9")]
    [KnownType(typeof(SitemapUrl))]
    public sealed class SitemapUrl
    {
        /// <summary>
        ///     Gets or sets the <see cref="ChangeFrequency" /> of the URL.
        /// </summary>
        [DataMember(Name = "changefreq", EmitDefaultValue = false)]
        public ChangeFrequency? ChangeFrequency { get; set; }

        /// <summary>
        ///     Gets or sets the last modified value for the URL.
        /// </summary>
        [DataMember(Name = "lastmod", EmitDefaultValue = false)]
        public string? LastModified { get; set; }

        /// <summary>
        ///     Gets or sets the location of the page. This is the absolute URL of the page.
        /// </summary>
        [DataMember(Name = "loc")]
        public string? Location { get; set; }

        /// <summary>
        ///     Gets or sets the relative priority of the page. This should be a value in the range [0.0, 1.0].
        /// </summary>
        [DataMember(Name = "priority", EmitDefaultValue = false)]
        public decimal? Priority { get; set; }
    }
}