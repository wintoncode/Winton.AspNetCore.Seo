// Copyright (c) Winton. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System;

namespace Winton.AspNetCore.Seo.HeaderMetadata.OpenGraph
{
    /// <summary>
    ///     Attibute that can be appled to a property representing an Open Graph property to specify the name
    ///     that should be used when serializing to meta tags.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class OpenGraphPropertyAttribute : Attribute
    {
        private string _name;

        /// <summary>
        ///     Gets or sets a value indicating whether or not this is root property for an object
        /// </summary>
        public bool IsPrimary { get; set; }

        /// <summary>
        ///     Gets or sets the name to be used for the property.
        /// </summary>
        public string Name
        {
            get
            {
                return _name ?? string.Empty;
            }

            set
            {
                _name = value;
            }
        }
    }
}