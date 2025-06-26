using Finme.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finme.Infrastructure.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(User user);
        string GenerateToken(Administrator user);
    }
}
