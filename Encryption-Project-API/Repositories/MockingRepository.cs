using Encryption_Project_LIB.DTOs;
using Encryption_Project_LIB.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CSharpFunctionalExtensions;

namespace Encryption_Project_API.Repositories
{
    public class MockingRepository<T> : IEncryptedUserService where T : class
    {
        private List<EncryptedUser> _encryptedUsers = new List<EncryptedUser>();
        private List<Secret> _encryptedSecret = new List<Secret>();
        private readonly Encryption_Project_LIB.Interfaces.IHashAndSalting _hashAndSalting;
        private readonly Encryption_Project_LIB.Interfaces.IConverter _converter;
        public MockingRepository(Encryption_Project_LIB.Interfaces.IHashAndSalting hashingAndSalting, Encryption_Project_LIB.Interfaces.IConverter converter)
        {
            _hashAndSalting = hashingAndSalting;
            _converter = converter;
            InitData();
        }
        public void InitData()
        {
            _encryptedUsers = new List<EncryptedUser>()
           {
              new EncryptedUser()
              {
                BlockedOrNot = Encryption_Project_LIB.Enums.Blocked.Notblocked,
                Hash = _converter.GetString(_hashAndSalting.GetHash(_converter.ConvertJSON().FirstOrDefault().AdminPassword,
                _converter.ConvertJSON().FirstOrDefault().Salt)),
                Id  = 1,
                Priveleges = Encryption_Project_LIB.Enums.Privelege.Statesecret,
                Roles = Encryption_Project_LIB.Enums.Role.Admin,
                Salt = _converter.ConvertJSON().FirstOrDefault().Salt,
                Username = "Admin"
              }
           };

            _encryptedSecret = new List<Secret>()
            {
                new Secret
                {
                     Body = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Tincidunt eget nullam non nisi est sit amet facilisis magna. Nibh nisl condimentum id venenatis a. Lacus suspendisse faucibus interdum posuere lorem ipsum. Mi eget mauris pharetra et. Nullam eget felis eget nunc lobortis mattis. Arcu bibendum at varius vel pharetra vel turpis. Pellentesque eu tincidunt tortor aliquam nulla facilisi cras. Aliquam sem et tortor consequat id. In nisl nisi scelerisque eu ultrices vitae auctor. Dictum fusce ut placerat orci nulla pellentesque. Imperdiet sed euismod nisi porta lorem mollis. Sed viverra tellus in hac. Tincidunt vitae semper quis lectus nulla at volutpat diam. " +
                     "Non consectetur a erat nam at lectus. Etiam dignissim diam quis enim lobortis scelerisque fermentum dui. Suscipit tellus mauris a diam maecenas sed.",
                     Header = "Secret Info",
                     Id = 1,
                     PrivelegeLevel = Encryption_Project_LIB.Enums.Privelege.Vulnerableinfo,
                     Name = "FirstSecret"
                },
                                new Secret
                {
                     Body = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Tincidunt eget nullam non nisi est sit amet facilisis magna. Nibh nisl condimentum id venenatis a. Lacus suspendisse faucibus interdum posuere lorem ipsum. Mi eget mauris pharetra et. Nullam eget felis eget nunc lobortis mattis. Arcu bibendum at varius vel pharetra vel turpis. Pellentesque eu tincidunt tortor aliquam nulla facilisi cras. Aliquam sem et tortor consequat id. In nisl nisi scelerisque eu ultrices vitae auctor. Dictum fusce ut placerat orci nulla pellentesque. Imperdiet sed euismod nisi porta lorem mollis. Sed viverra tellus in hac. Tincidunt vitae semper quis lectus nulla at volutpat diam. " +
                     "Non consectetur a erat nam at lectus. Etiam dignissim diam quis enim lobortis scelerisque fermentum dui. Suscipit tellus mauris a diam maecenas sed.",
                     Header = "Secret Info",
                     Id = 2,
                     PrivelegeLevel = Encryption_Project_LIB.Enums.Privelege.Statesecret,
                     Name = "FirstSecret"
                }
            };
        }


        public Result Add(RegisterUserVM VM)
        {
            if (VM == null)
            {
                return Result.Failure("VM is empty");
            }
            else
            {

                var amountofusers = _encryptedUsers.Count;
                VM.User.Id = _encryptedUsers.Count + 1;
                Encryption_Project_LIB.Singletons.SaltsSingleton.GetSaltsSingleton().AssignNewSalt();
                VM.User.Salt = Encryption_Project_LIB.Singletons.SaltsSingleton.GetSaltsSingleton().GetSalt();
                VM.User.Hash = _converter.GetString(_hashAndSalting.GetHash(VM.password, VM.User.Salt));
                _encryptedUsers.Add(VM.User);
                if (_encryptedUsers.Count == amountofusers)
                {
                    return Result.Failure("User not added");
                }
                else
                {
                    return Result.Success();
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
                var currentStatus = _encryptedUsers.Where(a => a.Id == User.Id).FirstOrDefault().BlockedOrNot;
                if (currentStatus == Encryption_Project_LIB.Enums.Blocked.Blocked)
                {
                    return Result.Success("User is blocked");
                }
                else
                {
                    _encryptedUsers.Where(a => a.Id == User.Id).FirstOrDefault().BlockedOrNot = Encryption_Project_LIB.Enums.Blocked.Blocked;
                    if (_encryptedUsers.Where(a => a.Id == User.Id).FirstOrDefault().BlockedOrNot == currentStatus)
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
                var currentStatus = _encryptedUsers.Where(a => a.Id == User.Id).FirstOrDefault().BlockedOrNot;
                if (currentStatus == Encryption_Project_LIB.Enums.Blocked.Notblocked)
                {
                    return Result.Success("User is un blocked");
                }
                else
                {
                    _encryptedUsers.Where(a => a.Id == User.Id).FirstOrDefault().BlockedOrNot = Encryption_Project_LIB.Enums.Blocked.Notblocked;
                    if (_encryptedUsers.Where(a => a.Id == User.Id).FirstOrDefault().BlockedOrNot == currentStatus)
                    {
                        return Result.Failure("User is not un blocked");
                    }
                    else
                    {
                        return Result.Success("User is un blocked");
                    }
                }
            }
        }

        public List<Encryption_Project_LIB.DTOs.EncryptedUser> GetAllUsers()
        {
            return _encryptedUsers;
        }

        public Encryption_Project_LIB.DTOs.EncryptedUser GetUser(int id)
        {
            return _encryptedUsers.Where(a => a.Id == id).FirstOrDefault();
        }

        public List<Encryption_Project_LIB.DTOs.Secret> GetAllSecrets()
        {
            return _encryptedSecret;
        }
        public List<Encryption_Project_LIB.DTOs.Secret> GetSecrets(string name, Encryption_Project_LIB.Enums.Privelege privelege)
        {
            List<Encryption_Project_LIB.DTOs.Secret> results = _encryptedSecret.Where(a => a.Name == name).ToList();
            if (privelege == Encryption_Project_LIB.Enums.Privelege.Statesecret)
            {
                //results = results.Where(a => a.PrivelegeLevel == Encryption_Project_LIB.Enums.Privelege.Vulnerableinfo && a.Name == name).ToList();
                var resulstsstateinfo = results.Where(a => a.PrivelegeLevel == Encryption_Project_LIB.Enums.Privelege.Statesecret && a.Name == name).ToList();
                results = resulstsstateinfo;
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
                var secretcount = _encryptedSecret.Count;
                _encryptedSecret.Add(secret);
                if (secretcount != _encryptedSecret.Count)
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
