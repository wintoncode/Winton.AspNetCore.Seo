// Copyright (c) Winton. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Options;

namespace Winton.AspNetCore.Seo.Sitemaps
{
    internal sealed class SitemapFactory : ISitemapFactory
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IOptionsSnapshot<SeoOptions> _optionsSnapshot;

        public SitemapFactory(IOptionsSnapshot<SeoOptions> optionsSnapshot, IHttpContextAccessor contextAccessor)
        {
            _optionsSnapshot = optionsSnapshot;
            _contextAccessor = contextAccessor;
        }

        public Sitemap Create()
        {
            string baseUri = _contextAccessor
                .HttpContext
                .Request
                .GetEncodedUrl()
                .Replace(Constants.SitemapUrl, string.Empty);
            return
                new Sitemap(
                    _optionsSnapshot.Value.Sitemap.Urls?.Select(url => url.ToSitemapUrl(baseUri)) ??
                    Enumerable.Empty<SitemapUrl>());
        }
    }
}