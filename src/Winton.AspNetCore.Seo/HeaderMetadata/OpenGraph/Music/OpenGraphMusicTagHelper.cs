// Copyright (c) Winton. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

namespace Winton.AspNetCore.Seo.HeaderMetadata.OpenGraph.Music
{
    /// <inheritdoc />
    /// <summary>
    ///     An Open Graph tag helper for song types.
    /// </summary>
    [OpenGraphNamespace("music", "http://ogp.me/ns/music#")]
    public abstract class OpenGraphMusicTagHelper : OpenGraphTagHelper
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="OpenGraphMusicTagHelper" /> class.
        /// </summary>
        /// <param name="subtype">The subtype of Open Graph music object.</param>
        protected OpenGraphMusicTagHelper(string subtype)
            : base($"music.{subtype}")
        {
        }
    }
}