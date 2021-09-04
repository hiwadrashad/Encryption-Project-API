using Encryption_Project_LIB.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption_Project_LIB.Interfaces
{
    public interface IConverter
    {
        List<Root> ConvertJSON();
        byte[] GetBytes(string str);
        string GetString(byte[] bytes);
    }
}

