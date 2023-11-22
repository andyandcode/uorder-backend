using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class ActiveLogConfiguration : IEntityTypeConfiguration<ActiveLog>
    {
        public void Configure(EntityTypeBuilder<ActiveLog> builder)
        {
            builder.ToTable("ActiveLog");
            builder.HasKey(x => x.Id);
        }
    }
}