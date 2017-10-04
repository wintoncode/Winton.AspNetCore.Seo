// Copyright (c) Winton. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Microsoft.AspNetCore.Mvc;

namespace Winton.AspNetCore.Seo.HeaderMetadata
{
    /// <summary>
    /// View component that defines the SEO meta tags in the HTML head element.
    /// </summary>
    [ViewComponent(Name = "Winton.AspNetCore.Seo.HeaderMetadata")]
    public sealed class HeaderMetadataViewComponent : ViewComponent
    {
        /// <summary>
        /// Called to create the <see cref="ViewComponent"/> with the specified model.
        /// </summary>
        /// <param name="model">The data model to bind to the component.</param>
        /// <returns>The <see cref="IViewComponentResult"/>.</returns>
        public IViewComponentResult Invoke(HeaderMetadataViewModel model)
        {
            return View("Default", model);
        }
    }
}