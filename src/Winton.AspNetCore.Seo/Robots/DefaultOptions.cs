// Copyright (c) Winton. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.Collections.Generic;
using Microsoft.Extensions.Hosting;

namespace Winton.AspNetCore.Seo.Robots
{
    /// <summary>
    ///     A default delegate that configures the <see cref="RobotsTxtOptions" /> of the <see cref="SeoOptions" />.
    ///     It only allows robots in the production environment.
    /// </summary>
    internal static class DefaultOptions
    {
        internal static void Configure(SeoOptions options, IHostEnvironment env)
        {
            options.RobotsTxt.UserAgentRecords = new List<UserAgentRecord>
            {
                new UserAgentRecord
                {
                    DisallowAll = !env.IsProduction()
                }
            };
        }
    }
}