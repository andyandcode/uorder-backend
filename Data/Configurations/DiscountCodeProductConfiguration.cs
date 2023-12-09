using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class DiscountCodeProductConfiguration : IEntityTypeConfiguration<DiscountProduct>
    {
        public void Configure(EntityTypeBuilder<DiscountProduct> builder)
        {
            builder.ToTable("DiscountForDish");
            builder.HasKey(x => new { x.DishId, x.DiscountCodeId });

            builder.HasOne(x => x.Dish).WithMany(x => x.HasDiscountCodes).HasForeignKey(x => x.DishId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.DiscountCode).WithMany(x => x.ApplicableProductIds).HasForeignKey(x => x.DiscountCodeId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}