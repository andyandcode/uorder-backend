using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class DishMenuConfiguration : IEntityTypeConfiguration<DishMenu>
    {
        public void Configure(EntityTypeBuilder<DishMenu> builder)
        {
            builder.ToTable("DishOfMenu");
            builder.HasKey(x => new { x.MenuId, x.DishId });

            builder.HasOne(x => x.Menu).WithMany(x => x.Dishes).HasForeignKey(x => x.MenuId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.Dish).WithMany(x => x.Menus).HasForeignKey(x => x.DishId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}