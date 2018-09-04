// Copyright (c) Winton. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

namespace Winton.AspNetCore.Seo.HeaderMetadata.OpenGraph.Music
{
    /// <inheritdoc />
    /// <summary>
    ///     An Open Graph tag helper for music radio station types.
    /// </summary>
    public sealed class OpenGraphMusicRadioStationTagHelper : OpenGraphMusicTagHelper
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="OpenGraphMusicRadioStationTagHelper" /> class.
        /// </summary>
        public OpenGraphMusicRadioStationTagHelper()
            : base("radio_station")
        {
        }

        /// <summary>
        ///     Gets or sets the creator of this station. This is a URL of a page with og:type profile.
        /// </summary>
        public string Creator { get; set; }
    }
}