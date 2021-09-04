using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Encryption_Project_FrontEnd.Controllers
{
    public class ReturnController : Controller
    {
        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Block()
        {
            return View();
        }

        public IActionResult Unblock()
        {
            return View();
        }

    }
}
