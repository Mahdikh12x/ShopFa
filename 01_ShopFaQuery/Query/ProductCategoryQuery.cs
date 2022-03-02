using _0_Framework.Application;
using _01_ShopFaQuery.Contracts.Product;
using _01_ShopFaQuery.Contracts.ProductCategory;
using DiscountManagement.Infrastructure.EFCore;
using InventoryManagement.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infrastructure.EFCore;

namespace _01_ShopFaQuery.Query
{
    public class ProductCategoryQuery : IProductCategoryQuery
    {
        private readonly ShopContext _shopContext;
        private readonly InventoryContext _inventoryContext;
        private readonly DiscountContext _discountContext;
        public ProductCategoryQuery(ShopContext shopContext, InventoryContext inventoryContext, DiscountContext discountContext)
        {
            _shopContext = shopContext;
            _inventoryContext = inventoryContext;
            _discountContext = discountContext;
        }

        public List<ProductCategoryQueryModel> GetList()
        {
            return _shopContext.ProductCategories.Select(x => new ProductCategoryQueryModel
            {
                Id = x.Id,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Name = x.Name,
                Slug = x.Slug,

            }).ToList();
        }

        public List<ProductCategoryQueryModel> GetCategoriesWhitProducts()
        {
            var inventories = _inventoryContext.Inventory.Select(x => new { x.UnitePrice, x.ProductId }).ToList();
            var discounts = _discountContext.CustomerDiscounts.Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now).Select(x => new { x.ProductId, x.DiscountRate }).ToList();
            var categories = _shopContext.ProductCategories.Include(x => x.Products)!.ThenInclude(x => x.ProductCategory)
                .Select(x => new ProductCategoryQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Slug = x.Slug,
                    Products = x.Products!.Select(x => new ProductQueryModel
                        {
                            Name = x.Name,
                            Id = x.Id,
                            Picture = x.Picture,
                            PictureAlt = x.PictureAlt,
                            PictureTitle = x.PictureTitle,
                            Slug = x.Slug
                        }).ToList()
                }).ToList();


            

            foreach (var category in categories)
            {
                if (category.Products != null)
                    foreach (var product in category.Products)
                    {
                        var productInventory = inventories.FirstOrDefault(x => x.ProductId == product.Id);
                        if (productInventory != null)
                        {
                            var price = productInventory.UnitePrice;
                            product.Price = price.ToMoney();
                            var productDiscount = discounts.FirstOrDefault(x => x.ProductId == product.Id);
                            if (productDiscount != null)
                            {
                                var discount = productDiscount.DiscountRate;
                                product.DiscountRate = discount;
                                product.HasDiscount = discount > 0;

                                var discountAmount = Math.Round((discount * price) / 100);
                                product.PriceWithDiscount = (price - discountAmount).ToMoney();
                            }
                        }
                    }
            }
            return categories;
        }

        public ProductCategoryQueryModel? GetProductsCategoryBy(string slug)
        {
            var inventories = _inventoryContext.Inventory.Select(x => new { x.UnitePrice, x.ProductId }).ToList();
            var discounts = _discountContext.CustomerDiscounts.Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now).Select(x => new { x.ProductId, x.DiscountRate, x.EndDate }).ToList();

            var categories = _shopContext.ProductCategories.Include(x => x.Products)
                .Select(x => new ProductCategoryQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Picture = x.Picture,
                    Slug = x.Slug,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    MetaDescription = x.MetaDescription,
                    Keywords = x.Keywords
                }).FirstOrDefault(x => x.Slug == slug);


            categories!.Products = _shopContext.Products.Where(x => x.CategoryId == categories.Id)
                .Select(x => new ProductQueryModel
                {
                    Name = x.Name,
                    Id = x.Id,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Slug = x.Slug
                }).ToList();
            //var products = _shopContext.Products.Include(x => x.ProductCategory)
            //    .Where(x => x.ProductCategory.Slug == slug).Select(x => new ProductQueryModel
            //    {
            //        Id = x.Id,
            //        Name = x.Name,
            //        Category = x.ProductCategory.Name,
            //        Picture = x.Picture,
            //        PictureAlt = x.PictureAlt,
            //        PictureTitle = x.PictureTitle,
            //        Slug = x.Slug,
            //        CategorySlug = x.ProductCategory.Slug
            //    }).ToList();

            if (categories.Products == null) return categories;
            {
                foreach (var product in categories.Products!)
                {
                    var productInventory = inventories.FirstOrDefault(x => x.ProductId == product.Id);
                    if (productInventory == null) continue;
                    {
                        product.Price = productInventory.UnitePrice.ToMoney();
                        var productDiscount = discounts.FirstOrDefault(x => x.ProductId == product.Id);
                        if (productDiscount == null) continue;
                        product.DiscountEndDate = productDiscount.EndDate.ToDiscountFormat();
                        product.DiscountRate = productDiscount.DiscountRate;
                        product.HasDiscount = productDiscount.DiscountRate > 0;
                        var priceWithDiscount =
                            Math.Round((productDiscount.DiscountRate * productInventory.UnitePrice) / 100);
                        product.PriceWithDiscount = (productInventory.UnitePrice - priceWithDiscount).ToMoney();
                    }
                }
            }

            return categories;
        }


    }
}
