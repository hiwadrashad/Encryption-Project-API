using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Encryption_Project_LIB.Tokens
{
    public class JWT
    {
        public static string GenerateJWT(IEnumerable<Claim> claims)
        {
            var SCRTYkey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("d0435081-9a7c-4881-bd11-37e0f37e5eaa"));
            var TKNDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddDays(7),
                Issuer = "Encryption-Project",
                Audience = "API-Caller",
                Subject = new ClaimsIdentity(new ClaimsIdentity(claims)),
                SigningCredentials = new SigningCredentials(SCRTYkey, SecurityAlgorithms.HmacSha256Signature)
            };

            var TKNHandler = new JwtSecurityTokenHandler();
            var TOKEN = TKNHandler.CreateToken(TKNDescriptor);

            return TKNHandler.WriteToken(TOKEN);
        }
    }
}
