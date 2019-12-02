// Copyright (c) Winton. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.Collections.Generic;

namespace Winton.AspNetCore.Seo.Robots
{
    /// <summary>
    ///     The options used to determine how the robots.txt file is built.
    /// </summary>
    public interface IRobotsTxtOptions
    {
        /// <summary>
        ///     Gets a value indicating whether the URL to the sitemap should be included in the robots.txt file.
        /// </summary>
        bool AddSitemapUrl { get; }

        /// <summary>
        ///     Gets the collection of <see cref="UserAgentRecord" />s to put in the robots.txt file.
        /// </summary>
        IEnumerable<UserAgentRecord>? UserAgentRecords { get; }
    }
}