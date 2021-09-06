using Encryption_Project_LIB.Encryption;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption_Project_API.DataBases
{
    public class EncryptedUsersDatabase : DbContext
    {
        private readonly Encryption_Project_LIB.Interfaces.IHashAndSalting _hashAndSalting;
        private readonly Encryption_Project_LIB.Interfaces.IConverter _converter;
        public EncryptedUsersDatabase(DbContextOptions<EncryptedUsersDatabase> options, Encryption_Project_LIB.Interfaces.IHashAndSalting hashingAndSalting, Encryption_Project_LIB.Interfaces.IConverter converter) : base(options)
        {
            _hashAndSalting = hashingAndSalting;
            _converter = converter;
        }
        public DbSet<Encryption_Project_LIB.DTOs.EncryptedUser> EncryptedUsers { get; set; }
        public DbSet<Encryption_Project_LIB.DTOs.Secret> EncryptedSecret { get; set; }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            modelbuilder.Entity<Encryption_Project_LIB.DTOs.EncryptedUser>().HasData
            (
            new Encryption_Project_LIB.DTOs.EncryptedUser 
            {
                BlockedOrNot = Encryption_Project_LIB.Enums.Blocked.Notblocked, 
                Hash = _converter.GetString(_hashAndSalting.GetHash(_converter.ConvertJSON().FirstOrDefault().AdminPassword, 
                _converter.ConvertJSON().FirstOrDefault().Salt)),
                Id  = 1,
                Priveleges = Encryption_Project_LIB.Enums.Privelege.Topsecret,
                Roles = Encryption_Project_LIB.Enums.Role.Admin,
                Salt = _converter.ConvertJSON().FirstOrDefault().Salt,
                Username = "Admin"    
            }
            , new Encryption_Project_LIB.DTOs.EncryptedUser
            {
                BlockedOrNot = Encryption_Project_LIB.Enums.Blocked.Notblocked,
                Hash = _converter.GetString(_hashAndSalting.GetHash(_converter.ConvertJSON().FirstOrDefault().AdminPassword,
                _converter.ConvertJSON().FirstOrDefault().Salt)),
                Id = 2,
                Priveleges = Encryption_Project_LIB.Enums.Privelege.Topsecret,
                Roles = Encryption_Project_LIB.Enums.Role.Admin,
                Salt = _converter.ConvertJSON().FirstOrDefault().Salt,
                Username = "Admin"
            },
            new Encryption_Project_LIB.DTOs.EncryptedUser
            {
                BlockedOrNot = Encryption_Project_LIB.Enums.Blocked.Notblocked,
                Hash = _converter.GetString(_hashAndSalting.GetHash(_converter.ConvertJSON().FirstOrDefault().AdminPassword,
                _converter.ConvertJSON().FirstOrDefault().Salt)),
                Id = 3,
                Priveleges = Encryption_Project_LIB.Enums.Privelege.Topsecret,
                Roles = Encryption_Project_LIB.Enums.Role.Admin,
                Salt = _converter.ConvertJSON().FirstOrDefault().Salt,
                Username = "Admin"
            }, new Encryption_Project_LIB.DTOs.EncryptedUser
            {
                BlockedOrNot = Encryption_Project_LIB.Enums.Blocked.Notblocked,
                Hash = _converter.GetString(_hashAndSalting.GetHash(_converter.ConvertJSON().FirstOrDefault().AdminPassword,
                _converter.ConvertJSON().FirstOrDefault().Salt)),
                Id = 4,
                Priveleges = Encryption_Project_LIB.Enums.Privelege.Topsecret,
                Roles = Encryption_Project_LIB.Enums.Role.Admin,
                Salt = _converter.ConvertJSON().FirstOrDefault().Salt,
                Username = "Admin"
            }



            );

            modelbuilder.Entity<Encryption_Project_LIB.DTOs.Secret>().HasData(
                new Encryption_Project_LIB.DTOs.Secret
                {
                     Body = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                     Header = "Secret Info",
                     Id = 1,
                     PrivelegeLevel = Encryption_Project_LIB.Enums.Privelege.Vulnerableinfo,
                     Name = "FirstSecret"
                },
                new Encryption_Project_LIB.DTOs.Secret
                {
                    Body = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    Header = "Secret Info",
                    Id = 2,
                    PrivelegeLevel = Encryption_Project_LIB.Enums.Privelege.Vulnerableinfo,
                    Name = "FirstSecret"
                }, 
                new Encryption_Project_LIB.DTOs.Secret
                {
                    Body = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    Header = "Secret Info",
                    Id = 3,
                    PrivelegeLevel = Encryption_Project_LIB.Enums.Privelege.Vulnerableinfo,
                    Name = "FirstSecret"
                }, 
                new Encryption_Project_LIB.DTOs.Secret
                {
                    Body = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    Header = "Secret Info",
                    Id = 4,
                    PrivelegeLevel = Encryption_Project_LIB.Enums.Privelege.Vulnerableinfo,
                    Name = "FirstSecret"
                });
        }
    }
}
