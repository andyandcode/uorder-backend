using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.Note).HasMaxLength(500);
            builder.Property(x => x.TableId).IsRequired(false);
            builder.Property(x => x.Total).IsRequired();
            builder.Property(x => x.OrderStatus).IsRequired();
            builder.Property(x => x.PaymentStatus).IsRequired();

            builder.HasOne(x => x.Table).WithMany(x => x.Orders).HasForeignKey(x => x.TableId).OnDelete(DeleteBehavior.SetNull);
        }
    }
}