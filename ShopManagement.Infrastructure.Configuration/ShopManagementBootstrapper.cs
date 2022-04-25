using _0_Framework.Infrastructure;
using _01_ShopFaQuery.Contracts.Order;
using _01_ShopFaQuery.Contracts.Product;
using _01_ShopFaQuery.Contracts.ProductCategory;
using _01_ShopFaQuery.Contracts.Slide;
using _01_ShopFaQuery.Query;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopManagement.Application;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Application.Contracts.Slide;
using ShopManagement.Domain.OrderAgg;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagement.Domain.ProductPictureAgg;
using ShopManagement.Domain.Services;
using ShopManagement.Domain.SlideAgg;
using ShopManagement.Infrastructure.Account.ACL;
using ShopManagement.Infrastructure.ACL;
using ShopManagement.Infrastructure.Configuration.Permissions;
using ShopManagement.Infrastructure.EFCore;
using ShopManagement.Infrastructure.EFCore.Repository;

namespace ShopManagement.Infrastructure.Configuration
{
    public class ShopManagementBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionStrings)
        {
            services.AddTransient<IProductCategoryApplication, ProductCategoryApplication>();
            services.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();

            services.AddTransient<IProductApplication, ProductApplication>();
            services.AddTransient<IProductRepository, ProductRepository>();

            services.AddTransient<IProductPictureApplication, ProductPictureApplication>();
            services.AddTransient<IProductPictureRepository, ProductPictureRepository>();

            services.AddTransient<ISlideRepository, SlideRepository>();
            services.AddTransient<ISlideApplication, SlideApplication>();


            services.AddTransient<ISlideQuery, SlideQuery>();
            services.AddTransient<IProductCategoryQuery, ProductCategoryQuery>();
            services.AddTransient<IProductQuery, ProductQuery>();

            services.AddTransient<ICartService,CartService>();
            services.AddTransient<IPermissionExposure, ShopPermissionExposure>();

            services.AddTransient<IOrderApplication,OrderApplication>();
            services.AddTransient<IOrderRepository,OrderRepository>();

            services.AddTransient<IShopInventoryAcl,ShopInventoryAcl>();
            services.AddTransient<IShopAccountAcl,ShopAccountAcl>();

            services.AddSingleton<ICartInformation, CartInformation>();

            services.AddDbContext<ShopContext>(x => x.UseSqlServer(connectionStrings));
        }
    }
}
