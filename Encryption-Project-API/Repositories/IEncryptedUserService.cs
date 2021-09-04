using CSharpFunctionalExtensions;
using Encryption_Project_LIB.DTOs;
using Encryption_Project_LIB.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Encryption_Project_API.Repositories
{
    public interface IEncryptedUserService
    {
        Result Add(RegisterUserVM VM);
        Result Block(Encryption_Project_LIB.DTOs.EncryptedUser User);
        Result Unblock(Encryption_Project_LIB.DTOs.EncryptedUser User);
        List<Encryption_Project_LIB.DTOs.EncryptedUser> GetAllUsers();
        Encryption_Project_LIB.DTOs.EncryptedUser GetUser(int id);
        List<Encryption_Project_LIB.DTOs.Secret> GetAllSecrets();
        List<Encryption_Project_LIB.DTOs.Secret> GetSecrets(string name, Encryption_Project_LIB.Enums.Privelege privelege);
        Result AddSecret(Secret secret);
    }
}
