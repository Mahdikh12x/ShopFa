using AccountManagement.Domain.RoleAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountManagement.Infrastructure.EFCore.Mapping
{
    internal class RoleMapping : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(128).IsRequired(true);
            builder.Property(x=>x.Description).HasMaxLength(550).IsRequired(false);

            builder.HasMany(x=>x.Users).WithOne(x=>x.Role).HasForeignKey(x => x.RoleId);
        }
    }
}
