using Microsoft.AspNetCore.Mvc;

namespace Winton.AspNetCore.Seo.HeaderMetadata
{
    [ViewComponent(Name = "Winton.AspNetCore.Seo.HeaderMetadata")]
    public sealed class HeaderMetadataViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(HeaderMetadataViewModel model)
        {
            return View("Default", model);
        }
    }
}