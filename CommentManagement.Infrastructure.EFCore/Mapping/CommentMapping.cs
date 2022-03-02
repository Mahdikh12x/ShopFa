using CommentManagement.Domain.CommentAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommentManagement.Infrastructure.EFCore.Mapping
{
    public class CommentMapping:IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {

            builder.ToTable("Comments");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(550).IsRequired(true);
            builder.Property(x => x.Email).HasMaxLength(1500);
            builder.Property(x => x.Description).HasMaxLength(1500);

          
        }
    }
}
