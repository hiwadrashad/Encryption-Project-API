using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Encryption_Project_API.DataBases
{
    public class EncryptedUsersDatabase : DbContext
    {
        public EncryptedUsersDatabase(DbContextOptions<EncryptedUsersDatabase> options) : base(options)
        {
        }
        public DbSet<Encryption_Project_LIB.DTOs.EncryptedUser> EncryptedUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            modelbuilder.Entity<Encryption_Project_LIB.DTOs.EncryptedUser>().HasData
            (
            new Encryption_Project_LIB.DTOs.EncryptedUser { BlockedOrNot = Encryption_Project_LIB.Enums.Blocked.Notblocked }
            );
        }
    }
}
