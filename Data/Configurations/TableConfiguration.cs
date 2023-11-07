using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class TableConfiguration : IEntityTypeConfiguration<Table>
    {
        public void Configure(EntityTypeBuilder<Table> builder)
        {
            builder.ToTable("Tables");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.Desc).HasMaxLength(500);
            builder.Property(x => x.IsActive).IsRequired();
            builder.Property(x => x.Data).IsRequired();
            builder.Property(x => x.Name).IsRequired();

            builder.HasMany(x => x.Orders).WithOne(x => x.Table).HasForeignKey(x => x.TableId).OnDelete(DeleteBehavior.SetNull);
        }
    }
}