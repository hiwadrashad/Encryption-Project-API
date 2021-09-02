using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption_Project_LIB.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizationAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext CONTEXT)
        {
            //find claims property inside IPrincipal
            var UserId = CONTEXT.HttpContext.User.Claims.FirstOrDefault( a => a.Type == "UserId")?.Value;

            if (Convert.ToInt32(UserId) <= 0)
            {
                CONTEXT.Result = new JsonResult(string.Empty) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}
