// Copyright (c) Winton. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

namespace Winton.AspNetCore.Seo.HeaderMetadata.OpenGraph
{
    /// <inheritdoc />
    /// <summary>
    ///     A video as defined by Open Graph.
    /// </summary>
    public sealed class Video : Media
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Video" /> class.
        /// </summary>
        /// <param name="url">The url of the media Open Graph object.</param>
        public Video(string url)
            : base(url)
        {
        }

        /// <summary>
        ///     Gets or sets the height of the video in pixels.
        /// </summary>
        public int? Height { get; set; }

        /// <summary>
        ///     Gets or sets the width of the video in pixels.
        /// </summary>
        public int? Width { get; set; }
    }
}