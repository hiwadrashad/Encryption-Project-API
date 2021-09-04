using Encryption_Project_LIB.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption_Project_LIB.ViewModels
{
    public class RegisterUserVM
    {
        public EncryptedUser User { get; set; }
        public string password { get; set; }
    }
}
