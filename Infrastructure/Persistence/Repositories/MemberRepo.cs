using Application.Abstractions.Persistence;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Repositories
{
    public class MemberRepo(ApplicationDbContext context) : IMemberRepo
    {
        public Task CreateUser(MemberEntity entity)
        {
            context.Members.Add(entity);
            return Task.CompletedTask;
        }
    }
}
