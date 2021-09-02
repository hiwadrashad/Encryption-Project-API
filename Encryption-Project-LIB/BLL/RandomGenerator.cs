using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption_Project_LIB.BLL
{
    public class RandomGenerator : Encryption_Project_LIB.Interfaces.IRandomGenerator
    {
        public string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public int RandomNumber(int low, int high)
        {
            Random random = new Random();
            return random.Next(low, high);
        }
    }
}
