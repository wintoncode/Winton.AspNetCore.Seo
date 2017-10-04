// Copyright (c) Winton. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.Reflection;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace Winton.AspNetCore.Seo.HeaderMetadata
{
    internal static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddHeaderMetadata(this IServiceCollection services)
        {
            Assembly assembly = typeof(ServiceCollectionExtensions).GetTypeInfo().Assembly;
            var embeddedFileProvider = new EmbeddedFileProvider(assembly, assembly.GetName().Name);
            services.Configure<RazorViewEngineOptions>(options => { options.FileProviders.Add(embeddedFileProvider); });
            return services;
        }
    }
}