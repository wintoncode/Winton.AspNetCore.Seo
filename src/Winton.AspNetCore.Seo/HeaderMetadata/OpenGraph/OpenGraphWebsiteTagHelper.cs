// Copyright (c) Winton. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

namespace Winton.AspNetCore.Seo.HeaderMetadata.OpenGraph
{
    /// <inheritdoc />
    /// <summary>
    ///     An Open Graph tag helper for website type pages.
    /// </summary>
    public sealed class OpenGraphWebsiteTagHelper : OpenGraphTagHelper
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="OpenGraphWebsiteTagHelper" /> class.
        /// </summary>
        public OpenGraphWebsiteTagHelper()
            : base("website")
        {
        }
    }
}