// Copyright (c) Winton. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.Collections.Generic;

namespace Winton.AspNetCore.Seo.HeaderMetadata.OpenGraph
{
    /// <summary>
    ///     Class to represent data for locale Open Graph tags.
    ///     Values are of the format language_TERRITORY.
    /// </summary>
    public sealed class Locale
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Locale" /> class.
        /// </summary>
        /// <param name="primary">The primary locale.</param>
        public Locale(string primary)
        {
            Primary = primary;
        }

        /// <summary>
        ///     Gets or sets a collection of other locales this page is available in.
        /// </summary>
        public IEnumerable<string> Alternate { get; set; }

        /// <summary>
        ///     Gets the primary locale.
        ///     If not specified, Open Graph defaults to en_US.
        /// </summary>
        [OpenGraphProperty(IsPrimary = true)]
        public string Primary { get; }
    }
}