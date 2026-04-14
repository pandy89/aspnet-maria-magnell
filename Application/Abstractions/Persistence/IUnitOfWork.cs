using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Abstractions.Persistence
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken ct = default);

        //transaction
    }
}
