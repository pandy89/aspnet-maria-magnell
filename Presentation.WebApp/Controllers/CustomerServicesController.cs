using Microsoft.AspNetCore.Mvc;

namespace Presentation.WebApp.Controllers
{
    public class CustomerServicesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
