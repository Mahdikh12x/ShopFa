﻿using _0_Framework.Application;
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

        public EditProduct? GetDetails(long id)
        {
            var product = _context.Products?.Select(x => new EditProduct
            {
                Name = x.Name,
                CategoryId = x.CategoryId,
                Description = x.Description,
                Code = x.Code,
                Keywords = x.Keywords,
                Id = x.Id,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                MetaDescription = x.MetaDescription,
                ShortDescription = x.ShortDescription,
                Slug = x.Slug,

            }).AsNoTracking().FirstOrDefault(x => x.Id == id);
            return product;
        }

        public Product? GetProductWithCategory(long id)
        {
            return _context.Products?.Include(x => x.ProductCategory).FirstOrDefault(x => x.Id == id);
        }

        public List<ProductViewModel>? GetProducts()
        {
            return _context.Products?.Select(x => new ProductViewModel
            {
                Id = x.Id,
                Name = x.Name
            }).AsNoTracking().ToList();
        }


        public async Task<List<ProductViewModel>>? SearchAsync(ProductSearchModel searchModel)
        {
            var query =  _context.Products?.Include(x => x.ProductCategory).Select(x => new ProductViewModel
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                Category = x.ProductCategory.Name,
                CreationDate = x.CreationDate.ToFarsi(),
                Picture = x.Picture,
                CategoryId = x.CategoryId
            });

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query?.Where(x => x.Name!.Contains(searchModel.Name));

            if (!string.IsNullOrWhiteSpace(searchModel.Code))
                query = query?.Where(x => x.Code!.Contains(searchModel.Code));

            if (searchModel.CategoryId != 0)
                query = query?.Where(x => x.CategoryId == searchModel.CategoryId);


            return await query?.OrderByDescending(x => x.Id).AsNoTracking().ToListAsync()!;

        }
    }
}
