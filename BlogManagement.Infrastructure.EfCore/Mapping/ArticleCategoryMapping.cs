using BlogManagement.Domain.ArticleCategoryAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogManagement.Infrastructure.EfCore.Mapping
{
    internal class ArticleCategoryMapping:IEntityTypeConfiguration<ArticleCategory>
    {
        public void Configure(EntityTypeBuilder<ArticleCategory> builder)
        {
            builder.ToTable("ArticleCategories");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(500).IsRequired(true);
            builder.Property(x => x.Description).HasMaxLength(2000).IsRequired(true);
            builder.Property(x => x.Picture).HasMaxLength(1500).IsRequired(false);
            builder.Property(x => x.PictureAlt).HasMaxLength(255).IsRequired(false);
            builder.Property(x => x.PictureTitle).HasMaxLength(500).IsRequired(false);
            builder.Property(x => x.MetaDescription).HasMaxLength(150).IsRequired(true);
            builder.Property(x => x.Slug).HasMaxLength(300).IsRequired(true);
            builder.Property(x => x.Keywords).HasMaxLength(100).IsRequired(true);
            builder.Property(x => x.CanonicalAddress).HasMaxLength(1000).IsRequired(false);
            builder.Property(x => x.ShowOrder).IsRequired(true);


            builder.HasMany(x => x.Articles)
                .WithOne(x => x.Category).HasForeignKey(x => x.CategoryId);
        }
    }
}
