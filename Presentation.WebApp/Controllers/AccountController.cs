using Application.Services;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.WebApp.ViewModels;
using System.Security.Claims;
using Application.Abstractions.Authentication;

namespace Presentation.WebApp.Controllers;

[Authorize]
public class AccountController(IMemberService memberService) : Controller
{
    public async Task<IActionResult> My()
    {
        var email = User.Identity?.Name ?? string.Empty;

        var vm = new UpdateMemberVM();

        return View("My", vm); // ("My", email)
    }

    [HttpGet]
    public IActionResult MyAccount()
    {
        var vm = new UpdateMemberVM();
        return View("My", vm);
    }

    [HttpPost]
    public async Task <IActionResult> UpdateMember(UpdateMemberVM vm, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return View("My", vm);

        var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrWhiteSpace(userIdValue))
            return Unauthorized();

        var userId = Guid.Parse(userIdValue);

        var result = await memberService.UpdateMemberAsync(
            userId,
            vm.FirstName,
            vm.LastName,
            vm.PhoneNumber,
            ct);

        if (!result)
            return BadRequest();

        return RedirectToAction("My", "Account");           
    }

    //[HttpPost, ValidateAntiForgeryToken]
    //public async Task<IActionResult> Delete (CancellationToken ct = default)
    //{
    //    var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
    //    if (string.IsNullOrWhiteSpace(userIdValue))
    //        return Unauthorized();

    //    var userId = Guid.Parse(userIdValue);

    //    var result = await memberService.DeleteMemberAsync(userId, ct);
    //    if (!result)
    //        return BadRequest();

    //    await authService.SignOutAsync();
    //    return RedirectToAction("Index", "Home");
    //}
}
