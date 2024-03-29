﻿using System.Linq;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using _01_ShopFaQuery.Contracts.Comment;
using _01_ShopFaQuery.Contracts.Product;
using _01_ShopFaQuery.Contracts.ProductCategory;
using CommentManagement.Infrastructure.EFCore;
using DiscountManagement.Infrastructure.EFCore;
using InventoryManagement.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Domain.ProductPictureAgg;
using ShopManagement.Infrastructure.EFCore;

namespace _01_ShopFaQuery.Query
{
    public class ProductQuery : IProductQuery
    {
        private readonly ShopContext _shopContext;
        private readonly InventoryContext _inventoryContext;
        private readonly DiscountContext _discountContext;
        private readonly CommentContext _commentContext;
        public ProductQuery(ShopContext shopContext, InventoryContext inventoryContext, DiscountContext discountContext, CommentContext commentContext)
        {
            _shopContext = shopContext;
            _inventoryContext = inventoryContext;
            _discountContext = discountContext;
            _commentContext = commentContext;
        }


        public List<ProductQueryModel> GetArrivalsProducts()
        {
            var inventories = _inventoryContext.Inventory.Select(x => new { x.UnitePrice, x.ProductId }).ToList();
            var discounts = _discountContext.CustomerDiscounts.Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now).Select(x => new { x.ProductId, x.DiscountRate, x.EndDate }).ToList();

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

                }).OrderByDescending(x => x.Id).Take(6).ToList();

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

        public List<ProductQueryModel> SearchProduct(string value)
        {
            var inventories = _inventoryContext.Inventory
                .Select(x => new { x.UnitePrice, x.InStock, x.ProductId })
                 .ToList();
            var discounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new { x.DiscountRate, x.ProductId, x.EndDate }).ToList();

            var query = _shopContext.Products.Include(x => x.ProductCategory).Select(x => new ProductQueryModel
            {
                Id = x.Id,
                Name = x.Name,
                PictureTitle = x.PictureTitle,
                PictureAlt = x.PictureAlt,
                Picture = x.Picture,
                CategorySlug = x.ProductCategory.Slug,
                Category = x.ProductCategory.Name,
                ShortDescription = x.ShortDescription,
                Slug = x.Slug
            }).AsNoTracking();

            if (!string.IsNullOrWhiteSpace(value))
                query = query.Where(x => x.Name!.Contains(value) || x.ShortDescription!.Contains(value));
            var products = query.OrderByDescending(x => x.Id).ToList();

            foreach (var product in products)
            {
                var inventory = inventories.FirstOrDefault(x => x.ProductId == product.Id);
                if (inventory == null) continue;
                {
                    var price = inventory.UnitePrice;
                    product.Price = price.ToMoney();

                    var discount = discounts.FirstOrDefault(x => x.ProductId == product.Id);
                    if (discount == null) continue;
                    var productDiscount = discount.DiscountRate;
                    product.DiscountRate = productDiscount;
                    if (product.DiscountRate > 0 && discount.EndDate > DateTime.Now)
                    {
                        product.HasDiscount = true;

                    }
                    var discountWithAmount = Math.Round(price * productDiscount / 100);
                    product.PriceWithDiscount = (price - discountWithAmount).ToMoney();
                }


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
                }).FirstOrDefault(x => x.Slug == slug);

            product!.Pictures = _shopContext.ProductPictures.Where(x => x.ProductId == product.Id)
            .Select(x => new ProductPictureQueryModel
            {
                ProductId = x.ProductId,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle
            }).ToList();

            var productInventory = inventories.FirstOrDefault(x => x.ProductId == product.Id);

            if (productInventory == null) return product;
            {
                var price = productInventory.UnitePrice;
                if (product == null) return product;
                product.Price = price.ToMoney();
                product.DoublePrice = price;
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



            product.Comments = _commentContext.Comments.Where(x => !x.IsCanceled)
                .Where(x => x.IsConfirmed)
                .Where(x => x.Type == CommentType.Product).Where(x => x.OwnerRecordId == product.Id)
                .Select(x => new CommentQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    CreationDate = x.CreationDate.ToFarsi(),
                    Description = x.Description,

                }).OrderByDescending(x => x.Id).ToList();

            return product;
        }

        public List<CartItem>? CheckCartProductItems(List<CartItem>? cartItems)
        {
            var inventories = _inventoryContext.Inventory.ToList();

            foreach (var item in cartItems.Where(cartItem => inventories.Any(inv => inv.ProductId == cartItem.Id && inv.InStock)))
            {
                var inventoryItem = inventories.Find(x => x.ProductId == item.Id);
                if (inventoryItem != null) item.IsInStock = inventoryItem.CalculateCurrentCount() >= item.Count;
            }
            return cartItems;
        }
    }
}
