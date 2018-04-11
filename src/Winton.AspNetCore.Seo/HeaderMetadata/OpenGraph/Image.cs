// Copyright (c) Winton. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.Collections.Generic;

namespace Winton.AspNetCore.Seo.HeaderMetadata.OpenGraph
{
    /// <summary>
    ///     An image as defined by Open Graph.
    /// </summary>
    public sealed class Image : Media
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Image"/> class.
        /// </summary>
        /// <param name="url">The url of the media Open Graph object.</param>
        public Image(string url)
            : base(url)
        {
        }

        /// <summary>
        ///     Gets or sets a description of what is in the image (not a caption).
        ///     If the page specifies an image it should specify alt.
        /// </summary>
        public string Alt { get; set; }

        /// <summary>
        ///     Gets or sets the height of the image in pixels.
        /// </summary>
        public int? Height { get; set; }

        /// <summary>
        ///     Gets or sets the width of the image in pixels.
        /// </summary>
        public int? Width { get; set; }
    }
}