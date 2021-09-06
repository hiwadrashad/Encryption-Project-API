using Encryption_Project_LIB.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Encryption_Project_LIB.Extensionmethods;
using Encryption_Project_API.Repositories;

namespace Encryption_Project_FrontEnd.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IEncryptedUserService _encryptedUserService;
        public AuthenticationController(IEncryptedUserService encryptedUserService)
        {
            _encryptedUserService = encryptedUserService;
        }
        public IActionResult Login()
        {
            LoadingData dataDTO = new LoadingData();
            return View(dataDTO);
        }
        [HttpPost]
        public IActionResult Login(LoadingData DTO)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri($"https://localhost:44371/api/APIData/{DTO.Username}/{DTO.Password}");
            HttpResponseMessage response = client.GetAsync("").Result;
            if (response.IsSuccessStatusCode)
            {
                var USER = _encryptedUserService.GetAllUsers().Where(a => a.Username == DTO.Username).FirstOrDefault();
                TempData.Put<EncryptedUser>("login",USER);
                //return View("../Manipulation/GetSecret");
                return RedirectToAction("GetSecret","Manipulation");
            }
            else
            {
                return Problem("Incorrect input", statusCode: 401);

            }
        }
    }
}
