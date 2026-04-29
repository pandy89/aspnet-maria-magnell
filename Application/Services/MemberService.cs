using Application.Abstractions.Authentication;
using Application.Abstractions.Persistence;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

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

    public async Task<bool> UpdateMemberAsync(Guid userId, string firstName, string lastName, string phoneNumber, CancellationToken ct = default)
    {
        var member = await memberRepo.GetByIdAsync(userId, ct);
        if (member is null)
            return false;

        member.UpdateMember(firstName, lastName, phoneNumber);

        await uow.SaveChangesAsync(ct);

        return true;
    }


    public async Task<MemberEntity> GetMemberByIdAsync(Guid userId, CancellationToken ct = default)
    {
        return await memberRepo.GetByIdAsync(userId, ct);
    }
}
