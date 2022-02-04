using System.Linq;
using _0_Framework.Application;
using _01_ShopFaQuery.Contracts.Product;
using DiscountManagement.Infrastructure.EFCore;
using InventoryManagement.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;
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
            var discounts = _discountContext.CustomerDiscounts.Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now).Select(x => new { x.ProductId, x.DiscountRate }).ToList();

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
                        product.HasDiscount = product.DiscountRate > 0;
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
    }
}
