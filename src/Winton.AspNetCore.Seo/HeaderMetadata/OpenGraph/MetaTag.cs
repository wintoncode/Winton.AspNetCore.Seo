// Copyright (c) Winton. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.Collections.Generic;
using System.IO;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Winton.AspNetCore.Seo.HeaderMetadata.OpenGraph
{
    internal struct MetaTag
    {
        public MetaTag(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; }

        public string Value { get; }

        public override string ToString()
        {
            var tagBuilder = new TagBuilder("meta");
            tagBuilder.Attributes.Add("property", Name);
            tagBuilder.Attributes.Add("content", Value);
            using (var writer = new StringWriter())
            {
                tagBuilder.WriteTo(writer, HtmlEncoder.Default);
                return writer.ToString();
            }
        }
    }
}