using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class DishMediaConfiguration : IEntityTypeConfiguration<DishMedia>
    {
        public void Configure(EntityTypeBuilder<DishMedia> builder)
        {
            builder.ToTable("DishOfMedia");
            builder.HasKey(x => new { x.MediaId, x.DishId });

            builder.HasOne(x => x.Media).WithMany(x => x.DishMedias).HasForeignKey(x => x.MediaId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.Dish).WithMany(x => x.DishMedias).HasForeignKey(x => x.DishId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}