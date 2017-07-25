using Microsoft.AspNetCore.Mvc;

namespace Winton.AspNetCore.Seo.OpenGraph
{
    [ViewComponent(Name = "Winton.AspNetCore.Seo.OgMetadata")]
    public sealed class OgMetadataViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(OgMetadataViewModel model)
        {
            return View("Default", model);
        }
    }
}