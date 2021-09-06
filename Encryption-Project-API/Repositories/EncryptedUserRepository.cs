using CSharpFunctionalExtensions;
using Encryption_Project_LIB.DTOs;
using Encryption_Project_LIB.Interfaces;
using Encryption_Project_LIB.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Encryption_Project_API.Repositories
{
    public class EncryptedUserRepository<T> : IEncryptedUserService where T : class
    {
        private readonly Encryption_Project_API.DataBases.EncryptedUsersDatabase _encryptedUserDatabase;
        private readonly IConverter _converter;
        private readonly IHashAndSalting _hashAndsalting;
        public EncryptedUserRepository(DataBases.EncryptedUsersDatabase encryptedUsersDatabase, IConverter converter, IHashAndSalting hashAndSalting)
        {
            _encryptedUserDatabase = encryptedUsersDatabase;
            _converter = converter;
            _hashAndsalting = hashAndSalting;
        }

        public Result Add(RegisterUserVM VM)
        {
            if (VM == null)
            {
                return Result.Failure("VM is empty");
            }
            else
            {
                if (_encryptedUserDatabase.EncryptedUsers.Any(a => a.Username == VM.User.Username))
                {
                    return Result.Failure("User already exists");
                }
                else
                {
                    var amountofusers = _encryptedUserDatabase.EncryptedUsers.Count();
                    Encryption_Project_LIB.Singletons.SaltsSingleton.GetSaltsSingleton().AssignNewSalt();
                    VM.User.Salt = Encryption_Project_LIB.Singletons.SaltsSingleton.GetSaltsSingleton().GetSalt();
                    VM.User.Hash = _converter.GetString(_hashAndsalting.GetHash(VM.password, VM.User.Salt));
                    _encryptedUserDatabase.EncryptedUsers.Add(VM.User);
                    _encryptedUserDatabase.SaveChanges();
                    if (_encryptedUserDatabase.EncryptedUsers.Count() == amountofusers)
                    {
                        return Result.Failure("User not added");
                    }
                    else
                    {
                        return Result.Success();
                    }
                }
            }
        }

        public Result Block(Encryption_Project_LIB.DTOs.EncryptedUser User)
        {
            if (User == null)
            {
                return Result.Failure("User is not available");
            }
            else
            {
                var currentStatus = _encryptedUserDatabase.EncryptedUsers.Where(a => a.Id == User.Id).FirstOrDefault().BlockedOrNot;
                if (currentStatus == Encryption_Project_LIB.Enums.Blocked.Blocked)
                {
                    return Result.Success("User blocked");
                }
                else
                {
                    var UnupdatedUser = _encryptedUserDatabase.EncryptedUsers.Find(User);
                    User.BlockedOrNot = Encryption_Project_LIB.Enums.Blocked.Blocked;
                    User.Hash = UnupdatedUser.Hash;
                    User.Id = UnupdatedUser.Id;
                    User.Priveleges = UnupdatedUser.Priveleges;
                    User.Roles = UnupdatedUser.Roles;
                    User.Salt = UnupdatedUser.Salt;
                    User.Username = UnupdatedUser.Username;
                    _encryptedUserDatabase.Set<Encryption_Project_LIB.DTOs.EncryptedUser>().Attach(User);
                    _encryptedUserDatabase.Entry(User).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _encryptedUserDatabase.SaveChanges();
                    if (_encryptedUserDatabase.EncryptedUsers.Where(a => a.Id == User.Id).FirstOrDefault().BlockedOrNot == currentStatus)
                    {
                        return Result.Failure("User is not blocked");
                    }
                    else
                    {
                        return Result.Success("User is blocked");
                    }
                }
            }
        }

        public Result Unblock(Encryption_Project_LIB.DTOs.EncryptedUser User)
        {
            if (User == null)
            {
                return Result.Failure("User is not available");
            }
            else
            {
                var currentStatus = _encryptedUserDatabase.EncryptedUsers.Where(a => a.Id == User.Id).FirstOrDefault().BlockedOrNot;
                if (currentStatus == Encryption_Project_LIB.Enums.Blocked.Notblocked)
                {
                    return Result.Success("User blocked");
                }
                else
                {
                    var UnupdatedUser = _encryptedUserDatabase.EncryptedUsers.Find(User);
                    User.BlockedOrNot = Encryption_Project_LIB.Enums.Blocked.Notblocked;
                    User.Hash = UnupdatedUser.Hash;
                    User.Id = UnupdatedUser.Id;
                    User.Priveleges = UnupdatedUser.Priveleges;
                    User.Roles = UnupdatedUser.Roles;
                    User.Salt = UnupdatedUser.Salt;
                    User.Username = UnupdatedUser.Username;
                    _encryptedUserDatabase.Set<Encryption_Project_LIB.DTOs.EncryptedUser>().Attach(User);
                    _encryptedUserDatabase.Entry(User).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _encryptedUserDatabase.SaveChanges();
                    if (_encryptedUserDatabase.EncryptedUsers.Where(a => a.Id == User.Id).FirstOrDefault().BlockedOrNot == currentStatus)
                    {
                        return Result.Failure("User is not blocked");
                    }
                    else
                    {
                        return Result.Success("User is blocked");
                    }
                }
            }
        }

        public List<Encryption_Project_LIB.DTOs.EncryptedUser> GetAllUsers()
        {
            return _encryptedUserDatabase.Set<Encryption_Project_LIB.DTOs.EncryptedUser>().ToList();
        }

        public Encryption_Project_LIB.DTOs.EncryptedUser GetUser(int id)
        {
            return _encryptedUserDatabase.Set<Encryption_Project_LIB.DTOs.EncryptedUser>().ToList().Where(a => a.Id == id).FirstOrDefault();
        }

        public List<Encryption_Project_LIB.DTOs.Secret> GetAllSecrets()
        {
            return _encryptedUserDatabase.Set<Encryption_Project_LIB.DTOs.Secret>().ToList();
        }
        public List<Encryption_Project_LIB.DTOs.Secret> GetSecrets(string name, Encryption_Project_LIB.Enums.Privelege privelege)
        {
            List<Encryption_Project_LIB.DTOs.Secret> results = _encryptedUserDatabase.Set<Encryption_Project_LIB.DTOs.Secret>().ToList().Where(a => a.Name == name).ToList();
            if (privelege == Encryption_Project_LIB.Enums.Privelege.Statesecret)
            {
                results = results.Where(a => a.PrivelegeLevel == privelege && a.Name == name).ToList();
                results.AddRange(results.Where(a => a.PrivelegeLevel == Encryption_Project_LIB.Enums.Privelege.Vulnerableinfo && a.Name == name));
            }
            else
            {
                results = results.Where(a => a.PrivelegeLevel == privelege && a.Name == name).ToList();
            }

            return results;
        }

        public Result AddSecret(Secret secret)
        {
            if (secret == null)
            {
                return Result.Failure("secret is empty");
            }
            else
            {
                var secretcount = _encryptedUserDatabase.EncryptedSecret.Count();
                _encryptedUserDatabase.EncryptedSecret.Add(secret);
                _encryptedUserDatabase.SaveChanges();
                if (secretcount != _encryptedUserDatabase.EncryptedSecret.Count())
                {
                    return Result.Success("secret added");
                }
                else
                {
                    return Result.Failure("secret didn't got added");
                }
            }
        }
    }
}
