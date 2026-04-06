using Microsoft.AspNetCore.Mvc;

namespace Presentation.WebApp.Controllers
{
    public class NotFoundPageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
