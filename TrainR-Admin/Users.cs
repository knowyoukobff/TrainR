using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TrainR_Admin
{
    class Users : DbContext
    {
        public string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private static bool _created = false;

        public Users()
        {
            if (!_created)
            {
                _created = true;
                Database.EnsureCreated();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string encryptedPassword = PasswordHash.Encrypt("wsiz");

            modelBuilder.Entity<User>().HasData(
                    new User { Id = 1, Login = "admin", Password = encryptedPassword }
                );
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionbuilder)
        {
            optionbuilder.UseSqlite(@"Data Source=" + path + "\\TrainRUsers.db");
        }

        public DbSet<User> User { get; set; }
    }

    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
