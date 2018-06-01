// Copyright (c) Winton. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

namespace Winton.AspNetCore.Seo.HeaderMetadata.OpenGraph
{
    /// <inheritdoc />
    /// <summary>
    ///     Audio as defined by Open Graph.
    /// </summary>
    public sealed class Audio : Media
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Audio"/> class.
        /// </summary>
        /// <param name="url">The url of the media Open Graph object.</param>
        public Audio(string url)
            : base(url)
        {
        }
    }
}