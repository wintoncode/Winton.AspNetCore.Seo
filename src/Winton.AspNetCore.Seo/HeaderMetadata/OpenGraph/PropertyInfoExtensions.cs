// Copyright (c) Winton. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System;
using System.Reflection;

namespace Winton.AspNetCore.Seo.HeaderMetadata.OpenGraph
{
    internal static class PropertyInfoExtensions
    {
        internal static OpenGraphNamespaceAttribute GetOpenGraphNamespace(this PropertyInfo propertyInfo)
        {
            return propertyInfo
                .DeclaringType?
                .GetTypeInfo()
                .GetCustomAttribute<OpenGraphNamespaceAttribute>()
                   ?? throw
                       new Exception(
                           $"The type {propertyInfo.DeclaringType?.Name} that declares the property {propertyInfo.Name} is missing the required {nameof(OpenGraphNamespaceAttribute)}");
        }

        internal static OpenGraphProperty GetOpenGraphPropertyInfo(
            this PropertyInfo propertyInfo,
            OpenGraphTagHelper tagHelper)
        {
            return OpenGraphProperty.Create(propertyInfo, tagHelper);
        }
    }
}