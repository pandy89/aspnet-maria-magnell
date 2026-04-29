using Application.Abstractions.Authentication;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity;

public class IdentityAuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager) : IAuthService
{
    public async Task<Guid> CreateUserAsync(string email, string password)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email is requied", nameof(email));

        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("Password is requied", nameof(password));

        var user = new ApplicationUser
        {   
            Id = Guid.NewGuid(),
            UserName = email,
            Email = email,
            EmailConfirmed = true
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

    public async Task<bool> SignInWithPasswordAsync(string email, string password)
    {

       

        var result = await signInManager.PasswordSignInAsync(email, password, false, false);
        return result.Succeeded;


    }

    public async Task SignOutAsync()
        => await signInManager.SignOutAsync();

    //public async Task<bool> DeleteUserAsync(Guid id)
    //{
    //    var user = await userManager.FindByIdAsync(id.ToString());
    //    if (user is null)
    //        return false;

    //    var result = await userManager.DeleteAsync(user);
    //    return result.Succeeded;
    //}
}
