using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Accounts");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Username).IsRequired().IsUnicode(false).HasMaxLength(100);
            builder.Property(x => x.Password).IsRequired().IsUnicode(false).HasMaxLength(100);
            builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);

            builder.HasOne(x => x.Roles).WithMany(x => x.Accounts).HasForeignKey(x => x.RoleId).OnDelete(DeleteBehavior.SetNull);
        }
    }
}