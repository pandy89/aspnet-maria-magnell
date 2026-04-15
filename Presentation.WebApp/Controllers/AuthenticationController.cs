using Application.Abstractions.Authentication;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Presentation.WebApp.Models;
using Presentation.WebApp.ViewModels;

namespace Presentation.WebApp.Controllers;

public class AuthenticationController(IMemberService memberService, IAuthService authService) : Controller
{
    public IActionResult SignIn()
    {
        return View();
    }

    [HttpPost, ValidateAntiForgeryToken]
    public new async Task<IActionResult> SignOut()
    {
        await authService.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    #region SignUp

    [HttpGet("sign-up")]
    public IActionResult SignUp()
    {
        var vm = new SignUpVM
        {

        };

        return View(vm);
    }

    [HttpPost("sign-up"), ValidateAntiForgeryToken]
    public IActionResult SignUp(SignUpForm form)
    {
        if (!ModelState.IsValid)
            return View(new SignUpVM { Form = form });

        HttpContext.Session.SetString("Auth.SignUp.Email", form.Email);

        return RedirectToAction(nameof(RegisterPassword));        
    }

    [HttpGet("register-password")]
    public IActionResult RegisterPassword()
    {
        return View();
    }

    [HttpPost("register-password")]
    public async Task<IActionResult> RegisterPassword(RegisterPasswordForm form, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return View(new RegisterPasswordVM { Form = form });

        var email = HttpContext.Session.GetString("Auth.SignUp.Email");
        if (string.IsNullOrWhiteSpace(email))
            return RedirectToAction(nameof(SignUp));

        var guid = await memberService.RegisterMemberAsync(email, form.Password, ct);
        if (guid == Guid.Empty )
        {
            //TODO: Fel meddelande 
            return View(new RegisterPasswordVM { Form = form });
        }

        await authService.SignInAsync(guid);

        HttpContext.Session.Remove("Auth.SignUp.Email");

        return RedirectToAction("My", "Account");

    }

   
    #endregion
}
