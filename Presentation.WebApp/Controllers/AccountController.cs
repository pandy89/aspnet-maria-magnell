using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.WebApp.Controllers;

[Authorize]
public class AccountController : Controller
{
    public IActionResult My()
    {

        var email = User.Identity?.Name ?? string.Empty;


        return View("My", email);
    }
}
