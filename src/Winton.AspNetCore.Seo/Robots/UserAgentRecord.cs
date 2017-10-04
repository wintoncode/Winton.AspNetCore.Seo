// Copyright (c) Winton. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Winton.AspNetCore.Seo.Robots
{
    /// <summary>
    /// Class representing a user agent record in a robots.txt file.
    /// </summary>
    public sealed class UserAgentRecord
    {
        /// <summary>
        /// Gets or sets the urls to disallow for this <see cref="UserAgent"/>.
        /// </summary>
        public IEnumerable<string> Disallow { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether all URLs should be disallowed for this <see cref="UserAgent"/>.
        /// </summary>
        public bool DisallowAll { get; set; }

        /// <summary>
        /// Gets or sets a the <see cref="UserAgent"/> for which the record applies to. Defaults to <code>UserAgent.Any</code>.
        /// </summary>
        public UserAgent UserAgent { get; set; } = UserAgent.Any;

        internal string CreateRecord()
        {
            StringBuilder stringBuilder = new StringBuilder()
                .AppendLine($"User-agent: {UserAgent}");

            IEnumerable<string> disallowedUrls = DisallowAll
                ? new List<string> { "/" }
                : (Disallow ?? Enumerable.Empty<string>()).DefaultIfEmpty();

            foreach (string url in disallowedUrls)
            {
                stringBuilder.AppendLine($"Disallow: {url}");
            }

            return stringBuilder.ToString();
        }
    }
}