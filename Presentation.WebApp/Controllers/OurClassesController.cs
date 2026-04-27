using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.WebApp.Controllers
{
    [Authorize]
    public class OurClassesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
