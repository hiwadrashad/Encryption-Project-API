using CSharpFunctionalExtensions;
using Encryption_Project_API.Repositories;
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

    }
}
