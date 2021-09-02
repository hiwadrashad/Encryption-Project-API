using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption_Project_LIB.Interfaces
{
    public interface IHashAndSalting
    {
        byte[] GetHash(string password, string salt);
        bool CompareHash(string attemptedPassword, byte[] hash, string salt);
    }
}
