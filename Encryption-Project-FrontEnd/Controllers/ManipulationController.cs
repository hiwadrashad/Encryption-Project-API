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

namespace Encryption_Project_FrontEnd.Controllers
{
    public class ManipulationController : Controller
    {
        public async  Task<IActionResult> GetAllUsers()
        {
            HttpClient client = new HttpClient();
            var UserDTO = TempData.Get<Encryption_Project_LIB.DTOs.EncryptedUser>("login");
            TempData.Put<EncryptedUser>("login", UserDTO);
            client.BaseAddress = new Uri($"https://localhost:44371/api/APIData/GetAllUsers");
            HttpResponseMessage response = client.GetAsync("").Result;
            var body = await response.Content.ReadAsStringAsync();
            List<EncryptedUser> secrets = JsonConvert.DeserializeObject<List<EncryptedUser>>(body);
            ReturnVM<List<EncryptedUser>> VM = new Encryption_Project_LIB.ViewModels.ReturnVM<List<EncryptedUser>>();
            VM.ReturnType = secrets;
            return View(VM);
        }
        [HttpPost]
        public async Task<IActionResult> GetAllUsers(ReturnVM<List<Encryption_Project_LIB.DTOs.EncryptedUser>> VM)
        {
            HttpClient client = new HttpClient();
            var UserDTO = TempData.Get<Encryption_Project_LIB.DTOs.EncryptedUser>("login");
            TempData.Put<EncryptedUser>("login", UserDTO);
            client.BaseAddress = new Uri($"https://localhost:44371/api/APIData/GetAllUsers");
            HttpResponseMessage response = client.GetAsync("").Result;
            var body = await response.Content.ReadAsStringAsync();
            List<EncryptedUser> secrets = JsonConvert.DeserializeObject<List<EncryptedUser>>(body);
            secrets = secrets.Where(a => a.Username == VM.Query).ToList();
            VM.ReturnType = secrets;
            return View(VM);
        }


        public async Task<IActionResult> GetSecret()
        {
            HttpClient client = new HttpClient();
            var UserDTO = TempData.Get<Encryption_Project_LIB.DTOs.EncryptedUser>("login");
            TempData.Put<EncryptedUser>("login", UserDTO);
            client.BaseAddress = new Uri($"https://localhost:44371/api/APIData/GetSecret/{UserDTO.Id}/FirstSecret");
            HttpResponseMessage response = client.GetAsync("").Result;
            var body = await response.Content.ReadAsStringAsync();
            List<Secret> secrets = JsonConvert.DeserializeObject<List<Secret>>(body);
            ReturnVM<List<Secret>> VM = new Encryption_Project_LIB.ViewModels.ReturnVM<List<Secret>>();
            VM.ReturnType = secrets;
            return View(VM);
        }

        [HttpPost]
        public async Task<IActionResult> GetSecret(ReturnVM<List<Secret>> VM)
        {
            HttpClient client = new HttpClient();
            var UserDTO = TempData.Get<Encryption_Project_LIB.DTOs.EncryptedUser>("login");
            TempData.Put<EncryptedUser>("login", UserDTO);
            client.BaseAddress = new Uri($"https://localhost:44371/api/APIData/GetSecret/{UserDTO.Id}/{VM.Query}");
            HttpResponseMessage response = client.GetAsync("").Result;
            var body = await response.Content.ReadAsStringAsync();
            List<Secret> secrets = JsonConvert.DeserializeObject<List<Secret>>(body);
            VM.ReturnType = secrets;
            return View(VM);
        }
    }
}
