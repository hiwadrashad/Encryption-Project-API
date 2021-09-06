using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption_Project_LIB.ViewModels
{
    public class ReturnVM<T> where T : class
    {
        public string Query { get; set; }
        public T ReturnType { get; set; }
    }
}
