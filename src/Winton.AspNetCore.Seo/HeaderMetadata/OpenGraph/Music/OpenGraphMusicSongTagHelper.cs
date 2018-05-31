// Copyright (c) Winton. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;

namespace Winton.AspNetCore.Seo.HeaderMetadata.OpenGraph.Music
{
    /// <inheritdoc />
    /// <summary>
    ///     An Open Graph tag helper for song types.
    /// </summary>
    public sealed class OpenGraphMusicSongTagHelper : OpenGraphMusicTagHelper
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="OpenGraphMusicSongTagHelper" /> class.
        /// </summary>
        public OpenGraphMusicSongTagHelper()
            : base("song")
        {
        }

        /// <summary>
        ///     Gets or sets the albums this song belongs to.
        /// </summary>
        [OpenGraphProperty(Name = "album")]
        public IEnumerable<Album> Albums { get; set; }

        /// <summary>
        ///     Gets or sets the length of the song in seconds.
        /// </summary>
        public int? Duration { get; set; }

        /// <summary>
        ///     Gets or sets the product ID of this song.
        /// </summary>
        public string Isrc { get; set; }

        /// <summary>
        ///     Gets or sets the artists of this song. This is a URL of a page with og:type profile.
        /// </summary>
        [OpenGraphProperty(Name = "musician")]
        public IEnumerable<string> Musicians { get; set; }

        /// <summary>
        ///     Gets or sets the preview urls for this song.
        /// </summary>
        public IEnumerable<Audio> PreviewUrl { get; set; }

        /// <summary>
        ///     Gets or sets a time representing when the song was released.
        /// </summary>
        public DateTime? ReleaseDate { get; set; }

        /// <summary>
        ///     Gets or sets the type of the song's release; one of 'original_release', 're_release', or 'anthology'.
        /// </summary>
        public string ReleaseType { get; set; }
    }
}