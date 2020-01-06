// Copyright (c) Winton. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

namespace Winton.AspNetCore.Seo.HeaderMetadata.OpenGraph
{
    /// <summary>
    ///     The base class for all Open Graph media types.
    /// </summary>
    public abstract class Media
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Media" /> class.
        /// </summary>
        /// <param name="url">The url of the media Open Graph object.</param>
        protected Media(string url)
        {
            Url = url;
        }

        /// <summary>
        ///     Gets or sets the secure (https) URL of the media.
        /// </summary>
        public string? SecureUrl { get; set; }

        /// <summary>
        ///     Gets or sets the MIME type of the media.
        /// </summary>
        public string? Type { get; set; }

        /// <summary>
        ///     Gets the URL of the media.
        /// </summary>
        [OpenGraphProperty(IsPrimary = true)]
        public string? Url { get; }
    }
}