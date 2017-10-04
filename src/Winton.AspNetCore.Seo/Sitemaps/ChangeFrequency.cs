// Copyright (c) Winton. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.Runtime.Serialization;

namespace Winton.AspNetCore.Seo.Sitemaps
{
    /// <summary>
    ///     An enum of the values that a changefreq element in a sitemap can have. The change frequency indicates to search
    ///     engines how often a page is likely to change and at what frequency they should re-index the page.
    /// </summary>
    [DataContract]
    public enum ChangeFrequency
    {
        /// <summary>
        ///     The page always changes.
        /// </summary>
        [EnumMember(Value = "always")]
        Always,

        /// <summary>
        ///     The page changes hourly.
        /// </summary>
        [EnumMember(Value = "hourly")]
        Hourly,

        /// <summary>
        ///     The page changes daily.
        /// </summary>
        [EnumMember(Value = "daily")]
        Daily,

        /// <summary>
        ///     The page changes weekly.
        /// </summary>
        [EnumMember(Value = "weekly")]
        Weekly,

        /// <summary>
        ///     The page changes monthly.
        /// </summary>
        [EnumMember(Value = "monthly")]
        Monthly,

        /// <summary>
        ///     The page changes yearly.
        /// </summary>
        [EnumMember(Value = "yearly")]
        Yearly,

        /// <summary>
        ///     The page never changes.
        /// </summary>
        [EnumMember(Value = "never")]
        Never
    }
}