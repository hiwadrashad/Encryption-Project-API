using Encryption_Project_LIB.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption_Project_LIB.DTOs
{
    public class Secret
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Header { get; set; }
        public string Body { get; set; }
        public Privelege PrivelegeLevel { get; set; }

    }
}
