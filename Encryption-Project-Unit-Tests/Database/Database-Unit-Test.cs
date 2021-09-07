using CSharpFunctionalExtensions;
using Encryption_Project_API.Repositories;
using Encryption_Project_LIB.DTOs;
using Encryption_Project_LIB.ViewModels;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Encryption_Project_Unit_Tests.Database
{
    public class Database_Unit_Test
    {
        [Fact]
        public void ADD_TO_DATABASE_TEST()
        {
            Result RETURN = new Result();
            RegisterUserVM VM = new RegisterUserVM();
            VM.User = new Encryption_Project_LIB.DTOs.EncryptedUser();
            VM.User.Username = "TEST";
            VM.User.Roles = Encryption_Project_LIB.Enums.Role.Regular;
            VM.password = "TEST";
            VM.User.BlockedOrNot = Encryption_Project_LIB.Enums.Blocked.Notblocked;
            Mock<IEncryptedUserService> MOCK = new Mock<IEncryptedUserService>();
            MOCK.Setup(a => a.Add(VM)).Returns(RETURN);
            MOCK.Object.Add(VM);
            MOCK.Verify(a => a.Add(VM));
        }
        [Fact]
        public void GET_ALL_USERS_TEST()
        {
            List<Encryption_Project_LIB.DTOs.EncryptedUser> STANDARDRETURN = new List<Encryption_Project_LIB.DTOs.EncryptedUser>();
            Mock<IEncryptedUserService> MOCK = new Mock<IEncryptedUserService>();
            MOCK.Setup(a => a.GetAllUsers()).Returns(STANDARDRETURN);
            MOCK.Object.GetAllUsers();
            MOCK.Verify(a => a.GetAllUsers());

        }
        [Fact]
        public void GET_USER_TEST()
        {
            EncryptedUser STANDARDRETURN = new EncryptedUser();
            STANDARDRETURN.Username = "TEST";
            STANDARDRETURN.Roles = Encryption_Project_LIB.Enums.Role.Regular;
            STANDARDRETURN.BlockedOrNot = Encryption_Project_LIB.Enums.Blocked.Notblocked;
            Mock<IEncryptedUserService> MOCK = new Mock<IEncryptedUserService>();
            MOCK.Setup(a => a.GetUser(1)).Returns(STANDARDRETURN);
            MOCK.Object.GetUser(1);
            MOCK.Verify(a => a.GetUser(1));
        }
        [Fact]
        public void GET_ALL_SECRETS()
        {
            List<Secret> STANDARDRETURN = new List<Secret>();
            Mock<IEncryptedUserService> MOCK = new Mock<IEncryptedUserService>();
            MOCK.Setup(a => a.GetAllSecrets()).Returns(STANDARDRETURN);
            MOCK.Object.GetAllSecrets();
            MOCK.Verify(a => a.GetAllSecrets());
        }
        [Fact]
        public void GET_SECRET()
        {
            List<Secret> STANDARDRETURN = new List<Secret>();
            Mock<IEncryptedUserService> MOCK = new Mock<IEncryptedUserService>();
            MOCK.Setup(a => a.GetSecrets("FirstSecret", Encryption_Project_LIB.Enums.Privelege.Topsecret)).Returns(STANDARDRETURN);
            MOCK.Object.GetSecrets("FirstSecret", Encryption_Project_LIB.Enums.Privelege.Topsecret);
            MOCK.Verify(a => a.GetSecrets("FirstSecret", Encryption_Project_LIB.Enums.Privelege.Topsecret));
        }
        [Fact]
        public void BLOCK_TEST()
        {
            Result STANDARDRESULT = new Result();
            EncryptedUser USERINPUT = new EncryptedUser();
            USERINPUT.Username = "TEST";
            USERINPUT.Roles = Encryption_Project_LIB.Enums.Role.Regular;
            USERINPUT.BlockedOrNot = Encryption_Project_LIB.Enums.Blocked.Notblocked;
            Mock<IEncryptedUserService> MOCK = new Mock<IEncryptedUserService>();
            MOCK.Setup(a => a.Block(USERINPUT)).Returns(STANDARDRESULT);
            MOCK.Object.Block(USERINPUT);
            MOCK.Verify(a => a.Block(USERINPUT));
        }
        [Fact]
        public void UNBLOCK_TEST()
        {
            Result STANDARDRESULT = new Result();
            EncryptedUser USERINPUT = new EncryptedUser();
            USERINPUT.Username = "TEST";
            USERINPUT.Roles = Encryption_Project_LIB.Enums.Role.Regular;
            USERINPUT.BlockedOrNot = Encryption_Project_LIB.Enums.Blocked.Notblocked;
            Mock<IEncryptedUserService> MOCK = new Mock<IEncryptedUserService>();
            MOCK.Setup(a => a.Unblock(USERINPUT)).Returns(STANDARDRESULT);
            MOCK.Object.Unblock(USERINPUT);
            MOCK.Verify(a => a.Unblock(USERINPUT));
        }
        [Fact]
        public void ADD_SECRET()
        {
            Result STANDARDRESULT = new Result();
            Secret USERINPUT = new Secret();
            USERINPUT.Name = "TEST";
            USERINPUT.Body = "TEST";
            USERINPUT.Header = "TEST";
            USERINPUT.PrivelegeLevel = Encryption_Project_LIB.Enums.Privelege.Statesecret;
            Mock<IEncryptedUserService> MOCK = new Mock<IEncryptedUserService>();
            MOCK.Setup(a => a.AddSecret(USERINPUT)).Returns(STANDARDRESULT);
            MOCK.Object.AddSecret(USERINPUT);
            MOCK.Verify(a => a.AddSecret(USERINPUT));
        }
    }
}
