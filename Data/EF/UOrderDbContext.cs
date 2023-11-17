using Data.Configurations;
using Data.Entities;
using Data.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Data.EF
{
    public class UOrderDbContext : DbContext
    {
        public UOrderDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AccountConfiguration());
            modelBuilder.ApplyConfiguration(new DishConfiguration());
            modelBuilder.ApplyConfiguration(new DishMediaConfiguration());
            modelBuilder.ApplyConfiguration(new DishMenuConfiguration());
            modelBuilder.ApplyConfiguration(new MediaConfiguration());
            modelBuilder.ApplyConfiguration(new MenuConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderDetailConfiguration());
            modelBuilder.ApplyConfiguration(new SystemSettingConfiguration());
            modelBuilder.ApplyConfiguration(new TableConfiguration());
            modelBuilder.ApplyConfiguration(new ActiveLogConfiguration());

            modelBuilder.Seed();
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<DishMedia> DishMedias { get; set; }
        public DbSet<DishMenu> DishMenus { get; set; }
        public DbSet<Media> Medias { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<SystemSetting> SystemSettings { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<ActiveLog> ActiveLogs { get; set; }
    }
}