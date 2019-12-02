// Copyright (c) Winton. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

namespace Winton.AspNetCore.Seo.HeaderMetadata.OpenGraph.Videos
{
    /// <summary>
    ///     Represents an actor object in Open Graph.
    /// </summary>
    public sealed class Actor
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Actor" /> class.
        /// </summary>
        /// <param name="id">The Open Graph id of the actor object.</param>
        public Actor(string id)
        {
            Id = id;
        }

        /// <summary>
        ///     Gets the URL of a page with og:type profile.
        /// </summary>
        [OpenGraphProperty(IsPrimary = true)]
        public string? Id { get; }

        /// <summary>
        ///     Gets or sets the role the actor played.
        /// </summary>
        public string? Role { get; set; }
    }
}