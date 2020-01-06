// Copyright (c) Winton. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;

namespace Winton.AspNetCore.Seo.HeaderMetadata.OpenGraph.Videos
{
    /// <inheritdoc />
    /// <summary>
    ///     An Open Graph tag helper for video types.
    /// </summary>
    [OpenGraphNamespace("video", "http://ogp.me/ns/video#")]
    public abstract class OpenGraphVideoTagHelper : OpenGraphTagHelper
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="OpenGraphVideoTagHelper" /> class.
        /// </summary>
        /// <param name="subtype">The subtype of Open Graph video object.</param>
        protected OpenGraphVideoTagHelper(string subtype)
            : base($"video.{subtype}")
        {
        }

        /// <summary>
        ///     Gets or sets the actors in this movie.
        /// </summary>
        [OpenGraphProperty(Name = "actor")]
        public IEnumerable<Actor>? Actors { get; set; }

        /// <summary>
        ///     Gets or sets the directors of this movie. Each value is a URL of a page with og:type profile.
        /// </summary>
        [OpenGraphProperty(Name = "director")]
        public IEnumerable<string>? Directors { get; set; }

        /// <summary>
        ///     Gets or sets the length of the movie in seconds.
        /// </summary>
        public int? Duration { get; set; }

        /// <summary>
        ///     Gets or sets the release date of the movie.
        /// </summary>
        public DateTime? ReleaseDate { get; set; }

        /// <summary>
        ///     Gets or sets the tag words associated with this movie.
        /// </summary>
        [OpenGraphProperty(Name = "tag")]
        public IEnumerable<string>? Tags { get; set; }

        /// <summary>
        ///     Gets or sets the writers of this movie. Each value is a URL of a page with og:type profile.
        /// </summary>
        [OpenGraphProperty(Name = "writer")]
        public IEnumerable<string>? Writers { get; set; }
    }
}