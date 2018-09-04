// Copyright (c) Winton. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System;

namespace Winton.AspNetCore.Seo.HeaderMetadata.OpenGraph
{
    /// <inheritdoc />
    /// <summary>
    ///     An attibute that can be appled to a class representing an Open Graph object to specify the namespace
    ///     that should be used when serializing to meta tags.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class OpenGraphNamespaceAttribute : Attribute
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="OpenGraphNamespaceAttribute" /> class.
        /// </summary>
        /// <param name="alias">The alias for the namespace.</param>
        /// <param name="uri">The namespace URI for the Open Graph object.</param>
        public OpenGraphNamespaceAttribute(string alias, string uri)
        {
            Alias = alias;
            Uri = uri;
        }

        /// <summary>
        ///     Gets the alias for the namespace.
        /// </summary>
        public string Alias { get; }

        /// <summary>
        ///     Gets the namespace to be used for the object.
        /// </summary>
        public string Uri { get; }

        internal string ToPrefixValue()
        {
            return $"{Alias}: {Uri}";
        }
    }
}