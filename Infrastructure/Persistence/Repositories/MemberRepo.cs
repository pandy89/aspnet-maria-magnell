using Application.Abstractions.Persistence;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories;

public class MemberRepo(ApplicationDbContext context) : IMemberRepo
{
    public Task CreateUser(MemberEntity entity)
    {
        context.Members.Add(entity);
        return Task.CompletedTask;
    }

    public Task UpdateUser(MemberEntity entity)
    {
        context.Members.Update(entity);
    }

    public Task DeleteUser(MemberEntity entity)
    {
        context.Members.Remove(entity);
    }
}
