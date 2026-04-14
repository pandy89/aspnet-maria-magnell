using Application.Abstractions.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence;

public class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
{
    public async Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
       return await context.SaveChangesAsync(ct);
    }
}
