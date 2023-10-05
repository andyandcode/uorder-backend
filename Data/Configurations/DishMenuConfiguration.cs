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

            builder.HasOne(x => x.Menu).WithMany(x => x.DishMenus).HasForeignKey(x => x.MenuId);
            builder.HasOne(x => x.Dish).WithMany(x => x.DishMenus).HasForeignKey(x => x.DishId);
        }
    }
}