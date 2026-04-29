using Domain.Entities;

namespace Application.Abstractions.Persistence;

public interface IMemberRepo 
{
    Task CreateUser(MemberEntity entity);
    Task UpdateUser (MemberEntity entity);
    Task DeleteUser (MemberEntity entity);

    Task<MemberEntity?> GetByIdAsync(Guid id, CancellationToken ct = default);

}
