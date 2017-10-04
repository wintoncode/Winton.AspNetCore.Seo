// Copyright (c) Winton. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;

namespace Winton.AspNetCore.Seo.Robots
{
    /// <summary>
    ///     A default implementation of <see cref="IRobotsTxtOptions" /> that always shows the sitemap and allows robots only
    ///     in the production environment.
    /// </summary>
    internal sealed class DefaultRobotsTxtOptions : IRobotsTxtOptions
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public DefaultRobotsTxtOptions(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public bool AddSitemapUrl => true;

        public IEnumerable<UserAgentRecord> UserAgentRecords => new List<UserAgentRecord>
        {
            new UserAgentRecord
            {
                DisallowAll = !_hostingEnvironment.IsProduction()
            }
        };
    }
}