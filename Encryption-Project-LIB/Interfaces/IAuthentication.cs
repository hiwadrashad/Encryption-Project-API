using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption_Project_LIB.Interfaces
{
    public interface IAuthentication
    {
        string Login(string USERNAME, string PASSWORD);
    }
}
