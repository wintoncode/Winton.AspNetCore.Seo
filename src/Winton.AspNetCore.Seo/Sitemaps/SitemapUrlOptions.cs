// Copyright (c) Winton. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Flurl;

namespace Winton.AspNetCore.Seo.Sitemaps
{
    /// <summary>
    ///     Options to define the data used to configure a <see cref="SitemapUrl" />.
    /// </summary>
    public sealed class SitemapUrlOptions
    {
        /// <summary>
        ///     Gets or sets the relative priority of the URL in the sitemap. Should be a value in the range [0.0, 1.0].
        /// </summary>
        public decimal? Priority { get; set; }

        /// <summary>
        ///     Gets or sets the value of the relative URL.
        /// </summary>
        public string? RelativeUrl { get; set; }

        internal SitemapUrl ToSitemapUrl(string baseUri)
        {
            return new SitemapUrl
            {
                Location = baseUri.AppendPathSegment(RelativeUrl),
                Priority = Priority
            };
        }
    }
}