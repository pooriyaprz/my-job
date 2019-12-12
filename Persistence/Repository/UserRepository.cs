using Core.Entity;
using Core.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repository
{
    public class UserRepository : Repository<Users>, IUsersRepository

    {
        public UserRepository(MyContext context) : base(context)
        {

        }
        public MyContext UserContext => Context as MyContext;

        public string CreateToken(IdentityUser user)
        {
            var cliams = new Claim[]
           {
                new Claim(JwtRegisteredClaimNames.Sub,user.Id),
                new Claim(JwtRegisteredClaimNames.Sub,user.UserName)
           };
            var sigingkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is the security key"));
            var signincreadentials = new SigningCredentials(sigingkey, SecurityAlgorithms.HmacSha256);
            var jwt = new JwtSecurityToken(signingCredentials: signincreadentials, claims: cliams);
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
