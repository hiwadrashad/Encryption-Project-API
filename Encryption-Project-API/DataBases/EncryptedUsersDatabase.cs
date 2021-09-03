using Encryption_Project_LIB.Encryption;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
                Hash = _hashAndSalting.GetHash(_converter.ConvertJSON().FirstOrDefault().AdminPassword, 
                _converter.ConvertJSON().FirstOrDefault().Salt),
                Id  = 1,
                Priveleges = Encryption_Project_LIB.Enums.Privelege.Topsecret,
                Roles = Encryption_Project_LIB.Enums.Role.Admin,
                Salt = _converter.ConvertJSON().FirstOrDefault().Salt,
                Username = "Admin"    
            }
            );

            modelbuilder.Entity<Encryption_Project_LIB.DTOs.Secret>().HasData(
                new Encryption_Project_LIB.DTOs.Secret
                {
                     Body = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Tincidunt eget nullam non nisi est sit amet facilisis magna. Nibh nisl condimentum id venenatis a. Lacus suspendisse faucibus interdum posuere lorem ipsum. Mi eget mauris pharetra et. Nullam eget felis eget nunc lobortis mattis. Arcu bibendum at varius vel pharetra vel turpis. Pellentesque eu tincidunt tortor aliquam nulla facilisi cras. Aliquam sem et tortor consequat id. In nisl nisi scelerisque eu ultrices vitae auctor. Dictum fusce ut placerat orci nulla pellentesque. Imperdiet sed euismod nisi porta lorem mollis. Sed viverra tellus in hac. Tincidunt vitae semper quis lectus nulla at volutpat diam. " +
                     "Non consectetur a erat nam at lectus. Etiam dignissim diam quis enim lobortis scelerisque fermentum dui. Suscipit tellus mauris a diam maecenas sed.",
                     Header = "Secret Info",
                     Id = 1,
                     PrivelegeLevel = Encryption_Project_LIB.Enums.Privelege.Vulnerableinfo,
                     Name = "FirstSecret"
                });
        }
    }
}
