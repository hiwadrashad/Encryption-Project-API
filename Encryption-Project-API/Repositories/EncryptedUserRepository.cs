using Encryption_Project_LIB.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Encryption_Project_API.Repositories
{
    public class EncryptedUserRepository<T> : IEncryptedUserService where T : class
    {
        private readonly Encryption_Project_API.DataBases.EncryptedUsersDatabase _encryptedUserDatabase;
        public EncryptedUserRepository(DataBases.EncryptedUsersDatabase encryptedUsersDatabase)
        {
            _encryptedUserDatabase = encryptedUsersDatabase;
        }

        public void Add(Encryption_Project_LIB.DTOs.EncryptedUser User)
        {
            _encryptedUserDatabase.EncryptedUsers.Add(User);
            _encryptedUserDatabase.SaveChanges();
        }

        public void Block(Encryption_Project_LIB.DTOs.EncryptedUser User)
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
        }

        public void Unblock(Encryption_Project_LIB.DTOs.EncryptedUser User)
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
                results = results.Where(a => a.PrivelegeLevel == privelege).ToList();
                results.AddRange(results.Where(a => a.PrivelegeLevel == Encryption_Project_LIB.Enums.Privelege.Vulnerableinfo));
            }
            else
            {
                results = results.Where(a => a.PrivelegeLevel == privelege).ToList();
            }

            return results;
        }

        public void AddSecret(Secret secret)
        {
            _encryptedUserDatabase.EncryptedSecret.Add(secret);
            _encryptedUserDatabase.SaveChanges();
        }
    }
}
