using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class DishConfiguration : IEntityTypeConfiguration<Dish>
    {
        public void Configure(EntityTypeBuilder<Dish> builder)
        {
            builder.ToTable("Dishes");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(500);
            builder.Property(x => x.Desc).HasMaxLength(500);
            builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);
            builder.Property(x => x.QtyPerDay).IsRequired().HasDefaultValue(100);
            builder.Property(x => x.CreatedAt).IsRequired().HasDefaultValue(DateTime.Now);
            builder.Property(x => x.Type).IsRequired();
        }
    }
}