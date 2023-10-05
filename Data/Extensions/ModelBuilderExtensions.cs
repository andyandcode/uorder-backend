using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().HasData(new Account() { Id = "1", Username = "admin", Password = "admin", IsActive = true });
            modelBuilder.Entity<SystemSetting>().HasData(new SystemSetting() { Id = "1", ChefCount = 1 });
        }
    }
}