// Copyright (c) Winton. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;

namespace Winton.AspNetCore.Seo.HeaderMetadata.OpenGraph.Music
{
    /// <summary>
    ///     An Open Graph tag helper for album types.
    /// </summary>
    public sealed class OpenGraphMusicAlbumTagHelper : OpenGraphMusicTagHelper
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="OpenGraphMusicAlbumTagHelper" /> class.
        /// </summary>
        public OpenGraphMusicAlbumTagHelper()
            : base("album")
        {
        }

        /// <summary>
        ///     Gets or sets the artists of this album. This is a URL of a page with og:type profile.
        /// </summary>
        [OpenGraphProperty(Name = "musician")]
        public IEnumerable<string> Musicians { get; set; }

        /// <summary>
        ///     Gets or sets the release date of the album.
        /// </summary>
        public DateTime? ReleaseDate { get; set; }

        /// <summary>
        ///     Gets or sets the type of the album's release; one of 'original_release', 're_release', or 'anthology'.
        /// </summary>
        public string ReleaseType { get; set; }

        /// <summary>
        ///     Gets or sets the songs on this album.
        /// </summary>
        [OpenGraphProperty(Name = "song")]
        public IEnumerable<Song> Songs { get; set; }
    }
}