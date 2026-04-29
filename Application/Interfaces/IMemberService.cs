using Domain.Entities;

namespace Application.Interfaces;

public interface IMemberService
{
    Task<Guid> RegisterMemberAsync(string email, string password, CancellationToken ct = default);
    Task<bool> UpdateMemberAsync(Guid userId, string firstName, string lastName, string phoneNumber, CancellationToken ct = default);
    Task<MemberEntity> GetMemberByIdAsync(Guid userId, CancellationToken ct = default);
}
