// Copyright (c) Winton. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

namespace Winton.AspNetCore.Seo.HeaderMetadata.OpenGraph.Music
{
    /// <summary>
    ///     Represents a song object in Open Graph.
    /// </summary>
    public sealed class Song
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Song" /> class.
        /// </summary>
        /// <param name="url">The url of another Open Graph object with og:type music.song.</param>
        public Song(string url)
        {
            Url = url;
        }

        /// <summary>
        ///     Gets or sets the disc number of the album that this song is on.
        /// </summary>
        public int? Disc { get; set; }

        /// <summary>
        ///     Gets or sets the track number of the disc for this song.
        /// </summary>
        public int? Track { get; set; }

        /// <summary>
        ///     Gets the URL of a page with og:type music.song.
        /// </summary>
        [OpenGraphProperty(IsPrimary = true)]
        public string Url { get; }
    }
}