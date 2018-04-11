// Copyright (c) Winton. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System;
using System.Collections;
using System.Reflection;

namespace Winton.AspNetCore.Seo.HeaderMetadata.OpenGraph
{
    internal static class PropertyInfoExtensions
    {
        internal static OpenGraphNamespaceAttribute GetOpenGraphNamespace(this PropertyInfo propertyInfo)
        {
            var openGraphType = propertyInfo
                .DeclaringType
                .GetTypeInfo()
                .GetCustomAttribute<OpenGraphNamespaceAttribute>();

            if (openGraphType == null)
            {
                throw new Exception(
                    $"The type {propertyInfo.DeclaringType.Name} that declares the property {propertyInfo.Name} is missing the required {nameof(OpenGraphNamespaceAttribute)}");
            }

            return openGraphType;
        }

        internal static OpenGraphProperty GetOpenGraphPropertyInfo(this PropertyInfo propertyInfo, OpenGraphTagHelper tagHelper)
        {
            return OpenGraphProperty.Create(propertyInfo, tagHelper);
        }

        internal static string GetOpenGraphName(this PropertyInfo propertyInfo, string @namespace)
        {
            string CreateName(string name)
            {
                return $"{@namespace}:{name}";
            }

            var attribute = propertyInfo.GetCustomAttribute<OpenGraphPropertyAttribute>();
            if (attribute != null)
            {
                return attribute.IsPrimary ? @namespace : CreateName(attribute.Name);
            }

            return CreateName(propertyInfo.Name.ConvertTitleCaseToSnakeCase());
        }
    }
}