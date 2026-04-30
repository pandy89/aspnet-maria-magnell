using Application.Abstractions.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class MemberRepo(ApplicationDbContext context) : IMemberRepo
{
    public Task CreateUser(MemberEntity entity)
    {
        context.Members.Add(entity);
        return Task.CompletedTask;
    }

    public void UpdateUser(MemberEntity entity)
    {
        context.Members.Update(entity);
    }

    //public void DeleteUser(MemberEntity entity)
    //{
    //    context.Members.Remove(entity);
    //}

    public async Task<MemberEntity?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var result = await context.Members
            .FirstOrDefaultAsync(x => x.Id == id, ct);
        return result;

    }
}
