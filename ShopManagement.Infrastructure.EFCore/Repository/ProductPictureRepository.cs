using System.Globalization;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Domain.ProductPictureAgg;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class ProductPictureRepository : BaseRepository<long, ProductPicture>, IProductPictureRepository
    {
        private readonly ShopContext _context;
        public ProductPictureRepository(ShopContext context) : base(context)
        {
            _context = context;
        }

        public List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel)
        {
            var query = _context.ProductPictures.Include(x=>x.Product).Select(x => new ProductPictureViewModel
            {
                Id = x.Id,
                IsRemoved = x.IsRemoved,
                CreationDate = x.CreationDate.ToString(CultureInfo.InvariantCulture),
                Picture = x.Picture,
                Product = x.Product.Name,
                ProductId = x.ProductId
            }
            ).ToList();

            if (searchModel.ProductId != 0)
                query = query.Where(x => x.ProductId == searchModel.ProductId).ToList();

            return query.OrderByDescending(x => x.Id).ToList();
        }

        public EditProductPicture GetDetails(long id)
        {
            var productPicture = _context.ProductPictures.Select(
                    x => new EditProductPicture
                    {
                        Id = x.Id,
                        Picture = x.Picture,
                        PictureAlt = x.PictureAlt,
                        PictureTitle = x.PictureTitle,
                        ProductId = x.ProductId,
                        
                        
                    })
                .FirstOrDefault(x => x.Id==id);
            return productPicture;
        }
    }
}
