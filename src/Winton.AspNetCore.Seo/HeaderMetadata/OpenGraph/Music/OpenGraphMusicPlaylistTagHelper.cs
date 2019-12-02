// Copyright (c) Winton. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.Collections.Generic;
using System.Linq;

namespace Winton.AspNetCore.Seo.HeaderMetadata.OpenGraph.Music
{
    /// <inheritdoc />
    /// <summary>
    ///     An Open Graph tag helper for music playlist types.
    /// </summary>
    public sealed class OpenGraphMusicPlaylistTagHelper : OpenGraphMusicTagHelper
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="OpenGraphMusicPlaylistTagHelper" /> class.
        /// </summary>
        public OpenGraphMusicPlaylistTagHelper()
            : base("playlist")
        {
        }

        /// <summary>
        ///     Gets or sets the creator of this playlist. This is a URL of a page with og:type profile.
        /// </summary>
        public string? Creator { get; set; }

        /// <summary>
        ///     Gets the number of songs on this playlist.
        /// </summary>
        public int? SongCount => Songs?.Count();

        /// <summary>
        ///     Gets or sets the songs on this playlist.
        /// </summary>
        [OpenGraphProperty(Name = "song")]
        public IEnumerable<Song>? Songs { get; set; }
    }
}