// Copyright (c) Winton. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Winton.AspNetCore.Seo.Robots
{
    /// <summary>
    ///     Class representing a user agent record in a robots.txt file.
    /// </summary>
    public sealed class UserAgentRecord
    {
        /// <summary>
        ///     Gets or sets the URLs to disallow for this user agent.
        /// </summary>
        public IEnumerable<string>? Disallow { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether all URLs should be disallowed for this user agent.
        /// </summary>
        public bool DisallowAll { get; set; }

        /// <summary>
        ///     Gets or sets the URLs that should not be indexed by this user agent.
        /// </summary>
        public IEnumerable<string>? NoIndex { get; set; }

        /// <summary>
        ///     Gets or sets the user agent for which the record applies. Defaults to <c>"*"</c>.
        /// </summary>
        public string UserAgent { get; set; } = "*";

        /// <summary>
        ///     Creates a string representation of the <see cref="UserAgentRecord" />.
        /// </summary>
        /// <returns>The string.</returns>
        public override string ToString()
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

            foreach (string url in NoIndex ?? Enumerable.Empty<string>())
            {
                stringBuilder.AppendLine($"Noindex: {url}");
            }

            return stringBuilder.ToString();
        }
    }
}