using Application.Abstractions.Authentication;
using Application.Abstractions.Persistence;
using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class MemberService(IAuthService authService, IMemberRepo memberRepo, IUnitOfWork uow) : IMemberService
    {
        public async Task<Guid> RegisterMemberAsync(string email, string password, CancellationToken ct = default)
        {
            
            var emailExists = await authService.EmailExistsAsync(email);
            if (emailExists)
                return Guid.Empty;


            //TODO: begintransaction
            try
            {
                var userId = await authService.CreateUserAsync(email, password);



                var user = MemberEntity.Create(userId);
                await memberRepo.CreateUser(user);
                await uow.SaveChangesAsync(ct);

                return userId;


            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
