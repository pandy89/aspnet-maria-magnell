using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Abstractions.Authentication
{
    public interface IAuthService
    {
        Task<bool> EmailExistsAsync(string email);
        Task<Guid> CreateUserAsync(string email, string password);
        Task<bool> SignInAsync(Guid id);
        Task<bool> SignInWithPasswordAsync(string email, string password);
        Task SignOutAsync();
        Task<bool> DeleteUserAsync(Guid userId);
    }
}
