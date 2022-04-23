using _0_Framework.Infrastructure;
using _01_ShopFaQuery.Contracts.Inventory;
using _01_ShopFaQuery.Query;
using InventoryManagement.Application;
using InventoryManagement.Application.Contract.Inventory;
using InventoryManagement.Domain.InventoryAgg;
using InventoryManagement.Infrastructure.Configuration.Permissions;
using InventoryManagement.Infrastructure.EfCore;
using InventoryManagement.Infrastructure.EfCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryManagement.Infrastructure.Configuration
{
    public class InventoryManagementBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddTransient<IInventoryRepository,InventoryRepository>();
            services.AddTransient<IInventoryApplication, InventoryApplication>();

            services.AddTransient<IPermissionExposure, InventoryPermissionExposure>();
            services.AddTransient<IInventoryQuery, InventoryQuery>();
            services.AddDbContext<InventoryContext>(x => x.UseSqlServer(connectionString));
        }

    }
}