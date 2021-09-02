using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Encryption_Project_API.Repositories
{
    public class EncryptedUserRepository
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
    }
}
