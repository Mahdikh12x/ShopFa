using _0_Framework.Application;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class ProductCategoryRepository : BaseRepository<long, ProductCategory>, IProductCategoryRepository
    {
        private readonly ShopContext _context;

        public ProductCategoryRepository(ShopContext context) : base(context)
        {
            _context = context;
        }

        public EditProductCategory GetDetails(long id)
        {
            var productCategory = _context.ProductCategories.Select(r => new EditProductCategory
            {
                Id = r.Id,
                Name = r.Name,
                Description = r.Description,
                Keywords = r.Keywords,
                MetaDescription = r.MetaDescription,
                //Picture = r.Picture,
                PictureTitle = r.PictureTitle,
                PictureAlt = r.PictureAlt,
                Slug = r.Slug
            }).FirstOrDefault(x => x.Id == id);

            return productCategory ?? new EditProductCategory();
        }

        public List<ProductCategoryViewModel> GetProductCategories()
        {
            return _context.ProductCategories.Select(x =>new ProductCategoryViewModel
            {
                Id=x.Id,
                Name=x.Name
            }).AsNoTracking().ToList();
        }

        public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel)
        {
            var query = _context.ProductCategories.Select(x => new ProductCategoryViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Picture = x.Picture,
                CreationDate = x.CreationDate.ToFarsi()

            });

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(x => x.Name.Contains(searchModel.Name));

            return query.OrderByDescending(x=>x.Id).AsNoTracking().ToList();
        }

        public string GetCategoryWithSlug(long id)
        {
            return _context.ProductCategories.Select(x => new { x.Id, x.Slug }).FirstOrDefault(x => x.Id == id)!.Slug;
        }
    }
}
