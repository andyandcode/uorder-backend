using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class DiscountCodeConfiguration : IEntityTypeConfiguration<DiscountCode>
    {
        public void Configure(EntityTypeBuilder<DiscountCode> builder)
        {
            builder.ToTable("DiscountCodes");
            builder.HasKey(x => x.Id);

            builder.HasMany(g => g.Orders).WithOne(s => s.DiscountCode).HasForeignKey(s => s.DiscountCodeId).OnDelete(DeleteBehavior.SetNull);
        }
    }
}