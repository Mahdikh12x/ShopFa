using BlogManagement.Domain.ArticleAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogManagement.Infrastructure.EfCore.Mapping
{
    internal class ArticleMapping:IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.ToTable("Articles");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title).HasMaxLength(500).IsRequired(true);
            builder.Property(x => x.Body).IsRequired(true);
            builder.Property(x => x.ShortDescription).HasMaxLength(1500).IsRequired(true);
            builder.Property(x => x.Picture).HasMaxLength(1000).IsRequired(false);
            builder.Property(x => x.PictureAlt).HasMaxLength(255).IsRequired(false);
            builder.Property(x => x.PictureTitle).HasMaxLength(500).IsRequired(false);
            builder.Property(x => x.Slug).HasMaxLength(500).IsRequired(true);
            builder.Property(x => x.Keywords).HasMaxLength(100).IsRequired(true);
            builder.Property(x => x.MetaDescription).HasMaxLength(150).IsRequired(true);
            builder.Property(x => x.CanonicalAddress).HasMaxLength(1000).IsRequired(false);


            builder.HasOne(x=>x.Category).WithMany(x=>x.Articles).HasForeignKey(x=>x.CategoryId);
        }
    }
}
