using Encryption_Project_LIB.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Encryption_Project_LIB.BLL
{
    public class Authentication : IAuthentication
    {
        public string Login(string USERNAME, string PASSWORD)
        {
            //replace if statement with database check
            if (USERNAME == "USERNAME" && PASSWORD == "PASSWORD")
            {
                return Tokens.JWT.GenerateJWT(new List<Claim>
                {
                    new Claim("","")
                });
            }
            else
            {
                throw new UnauthorizedAccessException();
            }
        }
    }
}
