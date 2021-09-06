using Encryption_Project_LIB.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Encryption_Project_Unit_Tests.Encryption
{
    public class Encryption_And_Decryption_Test
    {
        [Fact]
        public void HASHING_PASSWORD_TEST()
        {
            Mock<IHashAndSalting> MOCK = new Mock<IHashAndSalting>();
            string PASSWORD = "PASSWORD";
            string SALT = "SALT";
            byte[] STANDARDRETURNVALUE = new byte[] { };
            MOCK.Setup(a => a.GetHash(PASSWORD, SALT)).Returns(STANDARDRETURNVALUE);
            byte[] RETURNVALUE = MOCK.Object.GetHash(PASSWORD,SALT);
            Assert.NotNull(RETURNVALUE);
        }

        [Fact]
        public void COMPARE_HASH_TEST()
        {
            Mock<IHashAndSalting> MOCK = new Mock<IHashAndSalting>();
            string PASSWORD = "PASSWORD";
            string SALT = "SALT";
            byte[] STANDARDRETURNVALUE = new byte[] { };
            MOCK.Setup(a => a.GetHash(PASSWORD, SALT)).Returns(STANDARDRETURNVALUE);
            byte[] RETURNVALUE = MOCK.Object.GetHash(PASSWORD, SALT);
            MOCK.Setup(a => a.CompareHash(PASSWORD, RETURNVALUE, SALT)).Returns(true);
            bool ASSERTION = MOCK.Object.CompareHash(PASSWORD,RETURNVALUE,SALT);
            Assert.True(ASSERTION);
        }

        [Fact]
        public void RFC2898_ENCRYPTION_TEST()
        {
            Mock<IRfc2898Encryption> MOCK = new Mock<IRfc2898Encryption>();
            string KEY = "abc123";
            string CIPHERTEXT = "ENCRYPTION TEST";
            string STANDARDRETURN = "ENCRYPTIONTEXT";
            MOCK.Setup(a => a.Encrypt(CIPHERTEXT, KEY)).Returns(STANDARDRETURN);
            string RETURNTEXT = MOCK.Object.Encrypt(CIPHERTEXT,KEY);
            Assert.NotNull(RETURNTEXT);
        }

        [Fact]
        public void RFC2898_DECRYPTION_TEST()
        {
            Mock<IRfc2898Encryption> MOCK = new Mock<IRfc2898Encryption>();
            string KEY = "abc123";
            string CIPHERTEXT = "ENCRYPTION TEST";
            string STANDARDRETURN = "ENCRYPTIONTEXT";
            MOCK.Setup(a => a.Encrypt(CIPHERTEXT, KEY)).Returns(STANDARDRETURN);
            string RETURNTEXT = MOCK.Object.Encrypt(CIPHERTEXT, KEY);
            MOCK.Setup(a => a.Decrypt(RETURNTEXT, KEY)).Returns(STANDARDRETURN);
            string DECRYPTEDTEXT = MOCK.Object.Decrypt(RETURNTEXT,KEY);
            Assert.NotNull(DECRYPTEDTEXT);
            
        }
   
    }
}
