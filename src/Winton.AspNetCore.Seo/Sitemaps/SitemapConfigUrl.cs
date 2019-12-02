// Copyright (c) Winton. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Flurl;

namespace Winton.AspNetCore.Seo.Sitemaps
{
    /// <summary>
    ///     Class to define the data used to configure a single <see cref="SitemapUrl" /> in the <see cref="ISitemapConfig" />.
    /// </summary>
    public sealed class SitemapConfigUrl
    {
        /// <summary>
        ///     Gets or sets the relative priority of the URL in the sitemap. Should be a value in the range [0.0, 1.0].
        /// </summary>
        public decimal? Priority { get; set; }

        /// <summary>
        ///     Gets or sets the value of the relative URL.
        /// </summary>
        public string? RelativeUrl { get; set; }

        /// <summary>
        ///     Converts this configuration into a <see cref="SitemapUrl" />.
        /// </summary>
        /// <param name="baseUri">The base URI to put before each <see cref="RelativeUrl" />.</param>
        /// <returns>A new <see cref="SitemapUrl" />.</returns>
        public SitemapUrl ToSitemapUrl(string baseUri)
        {
            return new SitemapUrl
            {
                Location = baseUri.AppendPathSegment(RelativeUrl),
                Priority = Priority
            };
        }
    }
}