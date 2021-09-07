using Encryption_Project_LIB.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Encryption_Project_LIB.Extensionmethods;
using Newtonsoft.Json;
using Encryption_Project_LIB.ViewModels;

namespace Encryption_Project_LIB.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizationAttribute : Attribute, IAuthorizationFilter
    {
        public object TempData { get; private set; }

        public async void OnAuthorization(AuthorizationFilterContext CONTEXT)
        {

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri($"https://localhost:44371/api/APIData/GetAllUsers");
            HttpResponseMessage response = client.GetAsync("").Result;
            var body = await response.Content.ReadAsStringAsync();
            List<EncryptedUser> secrets = JsonConvert.DeserializeObject<List<EncryptedUser>>(body);
            //find claims property inside IPrincipal
            var UserId = CONTEXT.HttpContext.User.Claims.FirstOrDefault( a => a.Type == "UserId")?.Value;
            if (UserId == null)
            {
                CONTEXT.Result = new JsonResult(string.Empty) { StatusCode = StatusCodes.Status401Unauthorized };
            }

            if (Convert.ToInt32(UserId) <= 0)
            {
                CONTEXT.Result = new JsonResult(string.Empty) { StatusCode = StatusCodes.Status401Unauthorized };
            }

            if (secrets.Where(a => a.Id == Convert.ToInt32(UserId)).FirstOrDefault().BlockedOrNot == Enums.Blocked.Blocked)
            {
                CONTEXT.Result = new JsonResult(string.Empty) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}
