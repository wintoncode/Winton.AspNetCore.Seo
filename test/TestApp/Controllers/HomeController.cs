using Microsoft.AspNetCore.Mvc;

namespace Winton.AspNetCore.Seo.TestApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}