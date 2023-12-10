using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Utilities.Common;

namespace Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        private static readonly IdGeneration item = new IdGeneration();

        public static void Seed(this ModelBuilder modelBuilder)
        {
            var adminId = item.Generator(GenerationType.Role);

            modelBuilder.Entity<Account>().HasData(new Account { Id = item.Generator(GenerationType.Account), Username = "admin", Password = HandleHashes.EndcodePwd("admin"), IsActive = true, CreatedAt = DateTime.Now, RoleId = adminId });
            modelBuilder.Entity<SystemSetting>().HasData(new SystemSetting { Id = "1", Domain = "https://localhost:7297" });

            modelBuilder
                .Entity<Role>()
                .HasData(
                new Role { Id = adminId, Name = "admin", Level = 1 },
                new Role { Id = item.Generator(GenerationType.Role), Name = "creator", Level = 2 },
                new Role { Id = item.Generator(GenerationType.Role), Name = "staff", Level = 3 });
        }
    }
}