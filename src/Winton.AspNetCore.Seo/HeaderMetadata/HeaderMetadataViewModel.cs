// Copyright (c) Winton. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

namespace Winton.AspNetCore.Seo.HeaderMetadata
{
    /// <summary>
    /// The view model used for the header metadata.
    /// </summary>
    public sealed class HeaderMetadataViewModel
    {
        /// <summary>
        /// Gets or sets the description of the website.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the image to use for the website when it is shared.
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// Gets or sets the title for the website.
        /// </summary>
        public string Title { get; set; }
    }
}