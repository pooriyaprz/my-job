using Core.Entity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.IRepository
{
    public interface IUsersRepository : IRepository<Users>

    {
      string CreateToken(IdentityUser user);

    }
}
