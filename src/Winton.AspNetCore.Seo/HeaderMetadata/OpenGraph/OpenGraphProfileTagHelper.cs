// Copyright (c) Winton. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

namespace Winton.AspNetCore.Seo.HeaderMetadata.OpenGraph
{
    /// <inheritdoc />
    /// <summary>
    ///     An Open Graph tag helper for profile type pages.
    /// </summary>
    [OpenGraphNamespace("profile", "http://ogp.me/ns/profile#")]
    public sealed class OpenGraphProfileTagHelper : OpenGraphTagHelper
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="OpenGraphProfileTagHelper" /> class.
        /// </summary>
        public OpenGraphProfileTagHelper()
            : base("profile")
        {
        }

        /// <summary>
        ///     Gets or sets the name normally given to an individual by a parent or self-chosen.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        ///     Gets or sets the gender of the individual.
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        ///     Gets or sets the name inherited from a family or marriage and by which the individual is commonly known.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        ///     Gets or sets a short unique string to identify the individual.
        /// </summary>
        public string Username { get; set; }
    }
}