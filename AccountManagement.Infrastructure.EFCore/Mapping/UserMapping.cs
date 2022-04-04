using AccountManagement.Domain.UserAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountManagement.Infrastructure.EFCore.Mapping
{
    internal class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.Fullname).HasMaxLength(100).IsRequired(true);
            builder.Property(x=>x.Username).HasMaxLength(100).IsRequired(true);
            builder.Property(x=>x.Password).HasMaxLength(1000).IsRequired(true);
            builder.Property(x=>x.ProfilePicture).HasMaxLength(1000).IsRequired(false);
            builder.Property(x=>x.Mobile).HasMaxLength(20).IsRequired(true);
        
            builder.HasOne(x=>x.Role).WithMany(x=>x.Users).HasForeignKey(x=>x.RoleId);
        }
    }
}
