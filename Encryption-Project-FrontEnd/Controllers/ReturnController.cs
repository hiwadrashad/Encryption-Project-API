using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Encryption_Project_LIB.Extensionmethods;
using Encryption_Project_LIB.DTOs;
using Newtonsoft.Json;
using Encryption_Project_LIB.ViewModels;
using Encryption_Project_LIB.Singletons;
using Encryption_Project_LIB.Encryption;
using Encryption_Project_LIB.Interfaces;
using System.Net;

namespace Encryption_Project_FrontEnd.Controllers
{
    public class ReturnController : Controller
    {
        private readonly IHashAndSalting _hashAndSalting;
        private readonly IConverter _converter;
        public ReturnController(IHashAndSalting hashAndSalting, IConverter converter)
        {
            this._hashAndSalting = hashAndSalting;
            this._converter = converter;
        }
        public IActionResult Register()
        {
            var UserDTO = TempData.Get<Encryption_Project_LIB.DTOs.EncryptedUser>("login");
            TempData.Put<EncryptedUser>("login", UserDTO);
            EncryptedUser NEWUSER = new EncryptedUser();
            RegisterUserVM VM = new RegisterUserVM();
            VM.User = NEWUSER;
            return View(VM);
        }

        [HttpPost]
        public IActionResult Register(RegisterUserVM VM)
        {
            var UserDTO = TempData.Get<Encryption_Project_LIB.DTOs.EncryptedUser>("login");
            TempData.Put<EncryptedUser>("login", UserDTO);
            SaltsSingleton.GetSaltsSingleton().AssignNewSalt();
            VM.User.Salt = SaltsSingleton.GetSaltsSingleton().GetSalt();
            VM.User.Hash = _converter.GetString(_hashAndSalting.GetHash(VM.password,VM.User.Salt));
            HttpClient client = new HttpClient();
            var JSONContent = JsonConvert.SerializeObject(VM);
            var buffer = System.Text.Encoding.UTF8.GetBytes(JSONContent);
            var BAContent = new ByteArrayContent(buffer);
            client.PostAsync($"https://localhost:44371/api/APIData", BAContent);
            return View(VM);
        }

        public async Task<IActionResult> Block(string id)
        {
            HttpClient client = new HttpClient();
            var UserDTO = TempData.Get<Encryption_Project_LIB.DTOs.EncryptedUser>("login");
            TempData.Put<EncryptedUser>("login", UserDTO);
            client.BaseAddress = new Uri($"https://localhost:44371/api/APIData/id");
            await client.GetAsync("");
            return View();
        }

        public async Task<IActionResult> Unblock(string id)
        {
            HttpClient client = new HttpClient();
            var UserDTO = TempData.Get<Encryption_Project_LIB.DTOs.EncryptedUser>("login");
            TempData.Put<EncryptedUser>("login", UserDTO);
            client.BaseAddress = new Uri($"https://localhost:44371/api/APIData/Unblock/id");
            await client.GetAsync("");
            return View();
        }

    }
}
