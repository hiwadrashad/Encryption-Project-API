using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption_Project_LIB.Singletons
{
    public class SaltsSingleton
    {
        private static SaltsSingleton _saltsSingleton;

        private static string SALT;

        private SaltsSingleton()
        {

        }

        public static SaltsSingleton GetSaltsSingleton()
        {
            if (_saltsSingleton == null)
            {
                _saltsSingleton = new SaltsSingleton();
                _saltsSingleton.AssignNewSalt();
            }
            return _saltsSingleton;
        }

        public void AssignNewSalt()
        {
            SALT = BLL.RandomGenerator.RandomString(BLL.RandomGenerator.RandomNumber(1,100));
        }

        public string GetSalt()
        {
            return SALT;
        }


    }
}
