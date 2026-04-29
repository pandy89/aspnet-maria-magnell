namespace Application.Interfaces;

public interface IMemberService
{
    Task<Guid> RegisterMemberAsync(string email, string password, CancellationToken ct = default);
    Task<bool> UpdateMemberAsync(Guid userId, string firstName, string lastName, string phoneNumber, CancellationToken ct = default);
    Task<bool> DeleteMemberAsync(Guid userId, CancellationToken ct = default);
}
