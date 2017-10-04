// Copyright (c) Winton. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.Text;
using Microsoft.AspNetCore.Mvc;
using Winton.AspNetCore.Seo.Robots;
using Winton.AspNetCore.Seo.Sitemaps;

namespace Winton.AspNetCore.Seo
{
    /// <summary>
    ///     ASP.Net Core <see cref="Controller" /> for exposing endpoints for SEO documents, such as robots.txt and
    ///     sitemap.xml.
    /// </summary>
    public sealed class SeoController : Controller
    {
        private readonly IRobotsTxtFactory _robotsTxtFactory;
        private readonly ISitemapFactory _sitemapFactory;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SeoController" /> class.
        /// </summary>
        /// <param name="robotsTxtFactory">The <see cref="IRobotsTxtFactory" /> for creating the robots.txt file.</param>
        /// <param name="sitemapFactory">The <see cref="ISitemapFactory" /> for creating the sitemap.xml file.</param>
        public SeoController(IRobotsTxtFactory robotsTxtFactory, ISitemapFactory sitemapFactory)
        {
            _robotsTxtFactory = robotsTxtFactory;
            _sitemapFactory = sitemapFactory;
        }

        /// <summary>
        ///     An endpoint that returns the robots.txt file.
        /// </summary>
        /// <returns>A <see cref="ContentResult" /> containing the robots.txt file.</returns>
        public IActionResult GetRobots()
        {
            return Content(_robotsTxtFactory.Create(), "text/plain", Encoding.UTF8);
        }

        /// <summary>
        ///     An endpoint that returns the sitemap.xml file.
        /// </summary>
        /// <returns>An <see cref="ObjectResult" /> that is deserialised as XML to return the sitemap.xml file.</returns>
        [Produces("text/xml")]
        public IActionResult GetSitemap()
        {
            return new ObjectResult(_sitemapFactory.Create());
        }
    }
}