using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Infrastructure.EFCore.Mapping
{
    internal class ProductCategoryMapping : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.ToTable("ProductCategories");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(255).IsRequired(true);
            builder.Property(x => x.Description).HasMaxLength(1000).IsRequired(false);
            builder.Property(x => x.Picture).HasMaxLength(1000).IsRequired(false);
            builder.Property(x => x.PictureAlt).HasMaxLength(255).IsRequired(false);
            builder.Property(x => x.PictureTitle).HasMaxLength(500).IsRequired(false);
            builder.Property(x => x.MetaDescription).HasMaxLength(150).IsRequired(true);
            builder.Property(x => x.Slug).HasMaxLength(300).IsRequired(true);
            builder.Property(x => x.Keywords).HasMaxLength(80).IsRequired(true);

            builder.HasMany(x => x.Products).WithOne(x => x.ProductCategory).HasForeignKey(x => x.CategoryId);

        }
    }
}
