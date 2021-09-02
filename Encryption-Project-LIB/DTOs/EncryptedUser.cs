using Encryption_Project_LIB.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption_Project_LIB.DTOs
{
    public class EncryptedUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Salt { get; set; }
        public byte[] Hash { get; set; }
        public Blocked BlockedOrNot { get; set; }
        public Privelege Priveleges { get; set; }
        public Role Roles { get; set; }
    }
}
