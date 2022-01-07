using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class ProductRepository : BaseRepository<long, Product>, IProductRepository
    {
        private readonly ShopContext _context;

        public ProductRepository(ShopContext context) : base(context)
        {
            _context = context;
        }

        public EditProduct GetDetails(long id)
        {
            var product = _context.Products.Select(x => new EditProduct
            {
                Name = x.Name,
                CategoryId = x.CategoryId,
                Description = x.Description,
                Picture = x.Picture,
                Code = x.Code,
                Keywords = x.Keywords,
                Id = x.Id,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                MetaDescription = x.MetaDescription,
                ShortDescription = x.ShortDescription,
                Slug = x.Slug,
                UnitPrice = x.UnitePrice

            }).FirstOrDefault(x => x.Id == id);
            return product;
        }

        public List<ProductViewModel> GetProducts()
        {
            return _context.Products.Select(x => new ProductViewModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
        }


        public List<ProductViewModel> Search(ProductSearchModel searchModel)
        {
            var query = _context.Products.Include(x => x.ProductCategory).Select(x => new ProductViewModel
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                Category = x.ProductCategory.Name,
                CreationDate = x.CreationDate.ToString(),
                Picture = x.Picture,
                IsInStock = x.IsInStock
                

            }).ToList();

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(x => x.Name.Contains(searchModel.Name)).ToList();

            if (!string.IsNullOrWhiteSpace(searchModel.Code))
                query = query.Where(x => x.Code.Contains(searchModel.Code)).ToList();

            if (searchModel.CategoryId != 0)
                query = query.Where(x => x.CategoryId == searchModel.CategoryId).ToList();

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
