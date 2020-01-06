// Copyright (c) Winton. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;

namespace Winton.AspNetCore.Seo.HeaderMetadata.OpenGraph
{
    /// <inheritdoc />
    /// <summary>
    ///     An Open Graph tag helper for book type pages.
    ///     This object type represents a book or publication.
    ///     This is an appropriate type for ebooks, as well as traditional paperback or hardback books.
    ///     Do not use this type to represent magazines.
    /// </summary>
    [OpenGraphNamespace("book", "http://ogp.me/ns/book#")]
    public sealed class OpenGraphBookTagHelper : OpenGraphTagHelper
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="OpenGraphBookTagHelper" /> class.
        /// </summary>
        public OpenGraphBookTagHelper()
            : base("book")
        {
        }

        /// <summary>
        ///     Gets or sets the authors of this book. Each value is a URL of a page with og:type profile.
        /// </summary>
        [OpenGraphProperty(Name = "author")]
        public IEnumerable<string>? Authors { get; set; }

        /// <summary>
        ///     Gets or sets the ISBN of this book.
        /// </summary>
        public string? Isbn { get; set; }

        /// <summary>
        ///     Gets or sets the date that the book was released.
        /// </summary>
        public DateTime? ReleaseDate { get; set; }

        /// <summary>
        ///     Gets or sets the tag words associated with this book.
        /// </summary>
        [OpenGraphProperty(Name = "tag")]
        public IEnumerable<string>? Tags { get; set; }
    }
}