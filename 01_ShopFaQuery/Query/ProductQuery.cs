using System.Linq;
using _0_Framework.Application;
using _01_ShopFaQuery.Contracts.Product;
using DiscountManagement.Infrastructure.EFCore;
using InventoryManagement.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.ProductPictureAgg;
using ShopManagement.Infrastructure.EFCore;

namespace _01_ShopFaQuery.Query
{
    public class ProductQuery : IProductQuery
    {
        private readonly ShopContext _shopContext;
        private readonly InventoryContext _inventoryContext;
        private readonly DiscountContext _discountContext;
        public ProductQuery(ShopContext shopContext, InventoryContext inventoryContext, DiscountContext discountContext)
        {
            _shopContext = shopContext;
            _inventoryContext = inventoryContext;
            _discountContext = discountContext;
        }


        public List<ProductQueryModel> GetArrivalsProducts()
        {
            var inventories = _inventoryContext.Inventory.Select(x => new { x.UnitePrice, x.ProductId }).ToList();
            var discounts = _discountContext.CustomerDiscounts.Where(x =>  x.StartDate < DateTime.Now && x.EndDate > DateTime.Now).Select(x => new { x.ProductId, x.DiscountRate,x.EndDate }).ToList();

            var products = _shopContext.Products.Include(x => x.ProductCategory)
                .Select(x => new ProductQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Slug = x.Slug,
                    Category = x.ProductCategory.Name,
                    CategorySlug = x.ProductCategory.Slug

                }).OrderByDescending(x=>x.Id).Take(6).ToList();

            foreach (var product in products)
            {

                var productPrice = inventories.FirstOrDefault(x => x.ProductId == product.Id);

                if (productPrice != null) 
                {
                    var price = productPrice.UnitePrice;
                        product.Price = price.ToMoney();
                    var productDiscount = discounts.FirstOrDefault(x => x.ProductId == product.Id);
                    if (productDiscount != null)
                    {
                     var discount = productDiscount.DiscountRate;
                        product.DiscountRate = discount;
                        if (product.DiscountRate > 0 && productDiscount.EndDate > DateTime.Now)
                        {
                            product.HasDiscount = true;

                        }
                        var discountWithAmount = Math.Round(price * discount / 100);
                        product.PriceWithDiscount = (price - discountWithAmount).ToMoney();
                    }
                }

                //var price = productInventory.UnitePrice;
                //product.Price = price.ToMoney();
                //var productDiscount = discounts.FirstOrDefault(x => x.ProductId == product.Id);
                //if (productDiscount == null) continue;
                //var discount = productDiscount.DiscountRate;
                //product.DiscountRate = discount;
                //product.HasDiscount = discount > 0;

                //var discountAmount = Math.Round((discount * price) / 100);
                //product.PriceWithDiscount = (price - discountAmount).ToMoney();
            }
            return products;
        }

        public ProductQueryModel? GetProduct(string slug)
        {
            var inventories = _inventoryContext.Inventory.Select(x => new { x.UnitePrice, x.InStock, x.ProductId })
                .ToList();
            var discounts = _discountContext.CustomerDiscounts
                .Select(x => new { x.DiscountRate, x.ProductId, x.EndDate }).ToList();

            var product = _shopContext.Products
                .Include(x => x.ProductCategory)
                .Include(x => x.ProductPictures)
                .Select(x => new ProductQueryModel
                {
                    Id = x.Id,
                    Slug = x.Slug,
                    Category = x.ProductCategory.Name,
                    CategoryId = x.CategoryId,
                    CategorySlug = x.ProductCategory.Slug,
                    Code = x.Code,
                    Description = x.Description,
                    Keywords = x.Keywords,
                    MetaDescription = x.MetaDescription,    
                    PictureAlt = x.PictureAlt,
                    Name = x.Name,
                    PictureTitle = x.PictureTitle,
                    Picture = x.Picture,
                    ShortDescription = x.ShortDescription,
                    Pictures = MapPictures(x.ProductPictures)

                }).FirstOrDefault(x => x.Slug == slug);


            var productInventory = inventories.FirstOrDefault(x => x.ProductId == product.Id);

            if (productInventory == null) return product;
            {
                var price = productInventory.UnitePrice;
                if (product == null) return product;
                product.Price = price.ToMoney();
                product.InStock = productInventory.InStock;
                var productDiscount = discounts.FirstOrDefault(x => x.ProductId == product.Id);
                if (productDiscount == null) return product;
                var discount = productDiscount.DiscountRate;
                product.DiscountRate = discount;
                product.DiscountEndDate = productDiscount.EndDate.ToDiscountFormat();
                if (product.DiscountRate > 0 && productDiscount.EndDate > DateTime.Now)
                {
                    product.HasDiscount = true;

                }
                var priceWithDiscount = Math.Round(price * discount / 100);
                product.PriceWithDiscount = (price - priceWithDiscount).ToMoney();
            }


            return product;
        }

        private static List<ProductPictureQueryModel> MapPictures(IEnumerable<ProductPicture> productPictures)
        {
            return productPictures.Select(x => new ProductPictureQueryModel
            {
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                ProductId = x.ProductId
            }).ToList();
        }
    }
}
