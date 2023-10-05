using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class MediaConfiguration : IEntityTypeConfiguration<Media>
    {
        public void Configure(EntityTypeBuilder<Media> builder)
        {
            builder.ToTable("Medias");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Desc).HasMaxLength(500);
            builder.Property(x => x.CreatedAt).IsRequired().HasDefaultValue(DateTime.Now);
        }
    }
}