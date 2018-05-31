// Copyright (c) Winton. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.Linq;
using System.Text;

namespace Winton.AspNetCore.Seo.HeaderMetadata.OpenGraph
{
    internal static class StringExtensions
    {
        internal static string ConvertTitleCaseToSnakeCase(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return str;
            }

            var stringBuilder = new StringBuilder();
            stringBuilder.Append(char.ToLowerInvariant(str.First()));
            foreach (char c in str.ToCharArray().Skip(1))
            {
                if (char.IsUpper(c))
                {
                    stringBuilder.Append("_");
                }

                stringBuilder.Append(char.ToLowerInvariant(c));
            }

            return stringBuilder.ToString();
        }
    }
}