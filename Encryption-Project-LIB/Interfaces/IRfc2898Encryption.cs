using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption_Project_LIB.Interfaces
{
    public interface IRfc2898Encryption
    {
        string Encrypt(string clearText, string EncryptionKey = "abc123");
        string Decrypt(string cipherText, string EncryptionKey = "abc123");
    }
}
