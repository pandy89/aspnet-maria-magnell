using Application.Abstractions.Persistence;

namespace Infrastructure.Persistence;

public class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
{
    public async Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
       return await context.SaveChangesAsync(ct);
    }
}
