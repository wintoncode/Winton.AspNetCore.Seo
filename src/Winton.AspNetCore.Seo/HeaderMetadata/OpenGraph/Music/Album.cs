// Copyright (c) Winton. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

namespace Winton.AspNetCore.Seo.HeaderMetadata.OpenGraph.Music
{
    /// <summary>
    ///     Represents an album object in Open Graph.
    /// </summary>
    public sealed class Album
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Album" /> class.
        /// </summary>
        /// <param name="url">The url of another Open Graph object with og:type music.album.</param>
        public Album(string url)
        {
            Url = url;
        }

        /// <summary>
        ///     Gets or sets the disc (within the album) that the song is on, defaulting to to '1'.
        /// </summary>
        public int? Disc { get; set; }

        /// <summary>
        ///     Gets or sets the position (within the given disc) of the song.
        /// </summary>
        public int? Track { get; set; }

        /// <summary>
        ///     Gets the URL of a page with og:type music.album.
        /// </summary>
        [OpenGraphProperty(IsPrimary = true)]
        public string Url { get; }
    }
}