// Copyright (c) Winton. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Winton.AspNetCore.Seo.HeaderMetadata.OpenGraph
{
    internal sealed class OpenGraphProperty
    {
        private OpenGraphProperty(string fullName, bool isPrimary, object value)
        {
            FullName = fullName;
            IsPrimary = isPrimary;
            Value = value;
        }

        internal string FullName { get; }

        internal bool IsPrimary { get; }

        internal string Name => FullName.Split(':').LastOrDefault();

        internal object Value { get; }

        internal static OpenGraphProperty Create(PropertyInfo propertyInfo, OpenGraphTagHelper tagHelper)
        {
            return Create(propertyInfo, tagHelper, propertyInfo.GetOpenGraphNamespace().Alias);
        }

        internal IEnumerable<MetaTag> ToMetaTags()
        {
            switch (Value)
            {
                case null:
                    return Enumerable.Empty<MetaTag>();
                case DateTime dateTime:
                    return new List<MetaTag> { new MetaTag(FullName, dateTime.ToString("o")) };
                case IConvertible convertible:
                    return new List<MetaTag> { new MetaTag(FullName, Convert.ToString(convertible)) };
                case IEnumerable<object> enumerable:
                    return enumerable.SelectMany(x => new OpenGraphProperty(FullName, IsPrimary, x).ToMetaTags());
                default:
                    return Value
                        .GetType()
                        .GetProperties()
                        .Select(p => Create(p, this))
                        .OrderByDescending(ogp => ogp.IsPrimary)
                        .ThenBy(ogp => ogp.Name)
                        .SelectMany(ogp => ogp.ToMetaTags());
            }
        }

        private static OpenGraphProperty Create(PropertyInfo propertyInfo, OpenGraphProperty parent)
        {
            return Create(propertyInfo, parent.Value, parent.FullName);
        }

        private static OpenGraphProperty Create(PropertyInfo propertyInfo, object parent, string parentPath)
        {
            object value = propertyInfo.GetValue(parent);
            var attribute = propertyInfo.GetCustomAttribute<OpenGraphPropertyAttribute>();
            if (attribute != null)
            {
                return Create(parentPath, attribute.Name, attribute.IsPrimary, value);
            }

            return Create(parentPath, propertyInfo.Name.ConvertTitleCaseToSnakeCase(), false, value);
        }

        private static OpenGraphProperty Create(string parentPath, string name, bool isPrimary, object value)
        {
            return new OpenGraphProperty(isPrimary ? parentPath : $"{parentPath}:{name}", isPrimary, value);
        }
    }
}
