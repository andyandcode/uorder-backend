using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.Accounts).WithOne(x => x.Roles).HasForeignKey(x => x.RoleId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}