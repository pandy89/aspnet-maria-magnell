using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IMemberService
    {
        Task<Guid> RegisterMemberAsync(string email, string password, CancellationToken ct = default);
    }
}
