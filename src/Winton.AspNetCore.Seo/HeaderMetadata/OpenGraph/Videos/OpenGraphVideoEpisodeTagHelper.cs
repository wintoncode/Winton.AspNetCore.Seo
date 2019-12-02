// Copyright (c) Winton. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

namespace Winton.AspNetCore.Seo.HeaderMetadata.OpenGraph.Videos
{
    /// <inheritdoc />
    /// <summary>
    ///     An Open Graph tag helper for video episode types.
    /// </summary>
    public sealed class OpenGraphVideoEpisodeTagHelper : OpenGraphVideoTagHelper
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="OpenGraphVideoEpisodeTagHelper" /> class.
        /// </summary>
        public OpenGraphVideoEpisodeTagHelper()
            : base("episode")
        {
        }

        /// <summary>
        ///     Gets or sets the series that this episode if part of. This is a URL of a page with og:type video.tv_show.
        /// </summary>
        public string? Series { get; set; }
    }
}