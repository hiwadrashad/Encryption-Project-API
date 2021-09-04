using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Encryption_Project_FrontEnd.Controllers
{
    public class ManipulationController : Controller
    {
        public IActionResult GetAllUsers()
        {
            return View();
        }

        public IActionResult GetSecret()
        {
            return View();
        }

    }
}
