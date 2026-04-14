using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Abstractions.Persistence;

public interface IMemberRepo 
{
    Task CreateUser(MemberEntity entity);
}
