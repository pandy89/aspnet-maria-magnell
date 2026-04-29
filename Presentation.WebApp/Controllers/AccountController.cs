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

        var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var userId = Guid.Parse(userIdValue!);

        var member = await memberService.GetMemberByIdAsync(userId);        

        var vm = new UpdateMemberVM
        {
            FirstName = member?.FirstName ?? string.Empty,
            LastName = member?.LastName ?? string.Empty,
            PhoneNumber = member?.PhoneNumber ?? string.Empty

        };

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
}
