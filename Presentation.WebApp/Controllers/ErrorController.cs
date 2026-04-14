using Microsoft.AspNetCore.Mvc;

namespace Presentation.WebApp.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        [Route("/Error/{statusCode}")]
        public IActionResult StatusCodeHandler(int statusCode)
        {
            return statusCode switch
            {
                
                404 => View("NotFound"),
                _ => View("ServerError")
            };
        }
    }
}
