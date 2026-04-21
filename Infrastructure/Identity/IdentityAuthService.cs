using Application.Abstractions.Authentication;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Identity;

public class IdentityAuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager) : IAuthService
{
    public async Task<Guid> CreateUserAsync(string email, string password)
    {
        //TODO: checka så email och password finns

        var user = new ApplicationUser
        {   
            Id = Guid.NewGuid(),
            UserName = email,
            Email = email
        };

        var result = await userManager.CreateAsync(user, password);
        if (!result.Succeeded)
            return Guid.Empty;

        return user.Id;
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        return await userManager.FindByEmailAsync(email) is not null;
    }


    public async Task<bool> SignInAsync(Guid id)
    {
        var user = await userManager.FindByIdAsync(id.ToString());
        if (user is null)
            return false;

        await signInManager.SignInAsync(user, isPersistent: false);
        return true;
    }


    public async Task SignOutAsync()
        => await signInManager.SignOutAsync();
}
