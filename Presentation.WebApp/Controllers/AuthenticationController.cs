using Application.Abstractions.Authentication;
using Application.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.WebApp.Models;
using Presentation.WebApp.ViewModels;
using System.Security.Claims;

namespace Presentation.WebApp.Controllers;

public class AuthenticationController(IMemberService memberService, IAuthService authService, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, ILogger<AuthenticationController> logger) : Controller
{

    //Generera SignIn-vy,hämtar ut vilka External providers vi har, visar dem på vår Signin-vy. 
    [HttpGet]
    public async Task<IActionResult> SignIn(string? returnUrl = null)
    {
        var schemes = await signInManager.GetExternalAuthenticationSchemesAsync();

        var vm = new SignInVM
        {
            ReturnUrl = returnUrl,
            ExternalProviders = [.. schemes.Select(x => x.Name)]
        };

        return View(vm);
    }

    [HttpPost, ValidateAntiForgeryToken] // ValidateAntiForgeryToken = skyddar mot postanrop, säkerställer att anropen kommer ifrån vår egna sida och inte är scriptattack från en annan flik i webbläsaren. 
    public new async Task<IActionResult> SignOut()
    {
        await authService.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    //Startar den externa providern och skickar oss till respektive provider.

    [HttpPost, ValidateAntiForgeryToken]  
    public IActionResult ExternalLogin(string provider, string? returnUrl = null)
    {
        if (string.IsNullOrWhiteSpace(provider))
            return RedirectToAction(nameof(SignIn), new { returnUrl });

        var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Authentication", new { returnUrl });
        var properties = signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);

        return Challenge(properties, provider);
    }

    // Vi kommer tillbaka till vår sida, hämtar info från providern. Beronde på om vi har ett lokalt konto så loggas vi in direkt, annars görs en verifiering. 

    [HttpGet]
    public async Task<IActionResult> ExternalLoginCallback(string? returnUrl = null, string? remoteError = null)
    {
        if (remoteError is not null)
        {
            logger.LogWarning("Remote error from provider: {Error}", remoteError);
            return ExternalLoginFailed(returnUrl);
        }

        var externalUser = await GetExternalUserInfo();
        if (externalUser is null)
            return ExternalLoginFailed(returnUrl);

        var (info, email) = externalUser.Value;

        //Kolla ifall befintlig användare finns i systemet, finns en sparad koppling?
        var result = await signInManager.ExternalLoginSignInAsync
        (
            info.LoginProvider,
            info.ProviderKey,
            isPersistent: false,
            bypassTwoFactor: true
        );

        if (result.Succeeded)
            return RedirectToLocal(returnUrl);

        // TODO: Hantera lockOut

        return await ExternalVerfication(email, returnUrl);
    }

    private async Task<IActionResult> ExternalVerfication(string email, string? returnUrl = null)
    {
        // TODO: Generea engångskod, spara i cb/cache, skicka via email.

        return View("VerifyExternalLogin", new VerifyExternalLoginVM
        {
            ReturnUrl = returnUrl,
            Email = email
        });
    }

#if DEBUG
    [HttpGet]
    public IActionResult TestVerifyExternalLogin()
    {
        return View("VerifyExternalLogin", new VerifyExternalLoginVM
        {
            Email = "test@domain.com",
            ReturnUrl = "/"
        });
    }
#endif

    // Vi verifiera oss och beronde om det är finns ett lokalt kontot så kopplas vi ihop med det lokala kontot annars skapar vi en ny external user.

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> VerifyExternalLogin(VerifyExternalLoginVM vm)
    {
        if (!ModelState.IsValid)
            return View("VerifyExternalLogin", vm);

        // TODO: Validera koden mot db/cache

        if(!string.Equals(vm.Code, "123456", StringComparison.Ordinal))
        {
            ModelState.AddModelError(nameof(vm.Code), "Fel kod.");
            return View("VerifyExternalLogin", vm);
        }

        var externalUser = await GetExternalUserInfo();
        if (externalUser is null)
            return ExternalLoginFailed(vm.ReturnUrl);

        var (info, email) = externalUser.Value;

        var existingUser = await userManager.FindByEmailAsync(email);
        if (existingUser is not null)
            return await LinkExistingUser(existingUser, info, vm.ReturnUrl);

        return await CreateExternalUser(email, info, vm.ReturnUrl);
    }

    // Länkar ihop lokalt konto med external.
    private async Task<IActionResult> LinkExistingUser(ApplicationUser user, ExternalLoginInfo info, string? returnUrl = null)
    {
        var result = await userManager.AddLoginAsync(user, info);
        if (!result.Succeeded)
        {
            logger.LogError("Failed to link {Provider} to {Email} : {Errors}",
                info.LoginProvider,
                user.Email,
                string.Join(",", result.Errors.Select(x => x.Description))
             );

            return ExternalLoginFailed(returnUrl);
        }
        await signInManager.SignInAsync(user, isPersistent: false);
        return RedirectToLocal(returnUrl);
    }

    // Skapar en ny external user.
    private async Task<IActionResult> CreateExternalUser(string email, ExternalLoginInfo info, string?  returnUrl = null)
    {
        var user = new ApplicationUser
        {
            UserName = email,
            Email = email,
            EmailConfirmed = true
        };

        var createResult = await userManager.CreateAsync(user);
        if (!createResult.Succeeded)
        {
            logger.LogError("Failed to create user {Email} : {Error}",
                email,
                string.Join(",", createResult.Errors.Select(x => x.Description))
            );

            return ExternalLoginFailed(returnUrl);

        }
        var linkResult = await userManager.AddLoginAsync(user, info);
        if (!linkResult.Succeeded)
        {
            logger.LogError("Failed to link {Provider} to {Email} : {Errors}",
                info.LoginProvider,
                user.Email,
                string.Join(",", linkResult.Errors.Select(x => x.Description))
             );

            return ExternalLoginFailed(returnUrl);
        }

        await signInManager.SignInAsync(user, isPersistent: false);
        return RedirectToLocal(returnUrl);
    }
    private async Task<(ExternalLoginInfo Info, string Email)?> GetExternalUserInfo()
    {
        var info = await signInManager.GetExternalLoginInfoAsync();
        if (info is null)
        {
            logger.LogWarning("External login info was null");
            return null;
        }

        var email = info.Principal.FindFirstValue(ClaimTypes.Email);
        if (string.IsNullOrWhiteSpace(email))
        {
            logger.LogWarning("No email claim from {Provider}", info.LoginProvider);
            return null;
        }
        return (info, email);
    }

    private RedirectToActionResult ExternalLoginFailed(string? returnUrl = null)
    {
        TempData["Error"] = "Inloggningen misslyckades, försök igen.";
        return RedirectToAction(nameof(SignIn), new { returnUrl }); 
    }

    private IActionResult RedirectToLocal(string? returnUrl = null)
    {
        if (Url.IsLocalUrl(returnUrl))
            return Redirect(returnUrl);

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
