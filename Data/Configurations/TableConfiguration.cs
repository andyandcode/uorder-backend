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
            builder.Property(x => x.CreatedAt).IsRequired().HasDefaultValue(DateTime.Now);
            builder.Property(x => x.Desc).HasMaxLength(500);
            builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);
            builder.Property(x => x.Data).IsRequired();
        }
    }
}