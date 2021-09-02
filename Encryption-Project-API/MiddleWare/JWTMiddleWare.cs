using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
namespace Encryption_Project_API.MiddleWare
{
    public class JWTMiddleWare
    {
        private readonly RequestDelegate REQUEST;
        public JWTMiddleWare(RequestDelegate Request)
        {
            REQUEST = Request;
        }

        public async Task Invoke(HttpContext context)
        {

            var TOKEN = context.Request.Headers["Autherization"].FirstOrDefault()?.Split(" ").Last();

            if (TOKEN == null)
            {
                await (REQUEST(context));
            }
            else
            {
                await AttachUserToContext(context, TOKEN);
                await REQUEST(context);
            }
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        private async Task AttachUserToContext(HttpContext context, string TOKEN)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            try
            {
                var TOKENHandler = new JwtSecurityTokenHandler();
                var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("d0435081-9a7c-4881-bd11-37e0f37e5eaa"));

                TOKENHandler.ValidateToken(TOKEN, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = securityKey,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out Microsoft.IdentityModel.Tokens.SecurityToken ValidateToken);

                var JWTTOKEN = (JwtSecurityToken)ValidateToken;
                if (context.User != null)
                {
                    var claims = new List<Claim>
                {
                   new Claim("UserId", JWTTOKEN.Claims.First(a => a.Type == "UserId").Value)
                };

                    var identity = new ClaimsIdentity(claims);
                    context.User.AddIdentity(identity);
                }
            }
            catch (Exception ex)
            {
                // no user added
            }
        }
    }
}
