using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Infrastructure.EFCore.Mapping
{
    public class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(255).IsRequired(true);
            builder.Property(x => x.Code).HasMaxLength(15).IsRequired(true);
            builder.Property(x => x.ShortDescripion).HasMaxLength(500).IsRequired(true);
            builder.Property(x => x.Picture).HasMaxLength(1000).IsRequired(false);
            builder.Property(x => x.PictureAlt).HasMaxLength(255).IsRequired(false);
            builder.Property(x => x.PictureTitle).HasMaxLength(500).IsRequired(false);
            builder.Property(x => x.Description).IsRequired(true);
            builder.Property(x => x.Slug).HasMaxLength(500).IsRequired(true);
            builder.Property(x => x.MetaDescription).HasMaxLength(150).IsRequired(true);
            builder.Property(x => x.Keywords).HasMaxLength(100).IsRequired(true);

            builder.HasOne(x => x.ProductCategory).WithMany(x => x.Products).HasForeignKey(x => x.CategoryId);
        }
    }
}
