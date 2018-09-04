// Copyright (c) Winton. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.Linq;
using System.Text;
using Flurl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;

namespace Winton.AspNetCore.Seo.Robots
{
    internal sealed class RobotsTxtFactory : IRobotsTxtFactory
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRobotsTxtOptions _options;

        public RobotsTxtFactory(IHttpContextAccessor httpContextAccessor, IRobotsTxtOptions options)
        {
            _httpContextAccessor = httpContextAccessor;
            _options = options;
        }

        public string Create()
        {
            var stringBuilder = new StringBuilder();
            foreach (UserAgentRecord userAgentRecord in _options.UserAgentRecords ??
                                                        Enumerable.Empty<UserAgentRecord>())
            {
                stringBuilder.AppendLine(userAgentRecord.ToString());
            }

            if (_options.AddSitemapUrl)
            {
                stringBuilder.AppendLine(CreateSitemapUrl());
            }

            return stringBuilder.ToString();
        }

        private string CreateSitemapUrl()
        {
            string baseUrl = _httpContextAccessor?.HttpContext?.Request?.GetEncodedUrl();
            Url sitemapUrl = (baseUrl ?? string.Empty).Replace(Constants.RobotsUrl, Constants.SitemapUrl);
            return $"Sitemap: {sitemapUrl}";
        }
    }
}