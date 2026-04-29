using Domain.Entities;

namespace Application.Abstractions.Persistence;

public interface IMemberRepo 
{
    Task CreateUser(MemberEntity entity);
    void UpdateUser (MemberEntity entity);
    void DeleteUser (MemberEntity entity);
    Task<MemberEntity?> GetByIdAsync(Guid id, CancellationToken ct = default);

}
