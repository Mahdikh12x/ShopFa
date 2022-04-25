using _0_Framework.Application;
using _01_ShopFaQuery.Contracts.Report;
using AccountManagement.Infrastructure.EFCore;
using InventoryManagement.Infrastructure.EfCore;
using ShopManagement.Infrastructure.EFCore;

namespace _01_ShopFaQuery.Query
{
    public class ReportQuery : IReportQuery
    {
        private readonly ShopContext _shopContext;
        private readonly InventoryContext _inventoryContext;
        private readonly AccountContext _accountContext;
        public ReportQuery(ShopContext shopContext, InventoryContext inventoryContext, AccountContext accountContext)
        {
            _shopContext = shopContext;
            _inventoryContext = inventoryContext;
            _accountContext = accountContext;
        }

        public List<ChartViewModel> GetReports()
        {
            var products = _shopContext.Products.Select(x => new { x.Name, x.Id }).ToList();
            var orders = _shopContext.Orders.Where(x => x.CreationDate <= DateTime.Today).ToList();
            var inventories = _inventoryContext.Inventory.Where(x => x.InStock).Take(10).ToList();
            var charts = new List<ChartViewModel>();

            return charts;
        }

        public ReportCountViewModel GetReportCounts()
        {
            var productCount = _shopContext.Products.Count();
            var orderCount = _shopContext.Orders.Count(x => !x.IsPayed && !x.IsCanceled);
            var userCount = _accountContext.Users.Count(x => x.IsActive);
            var orderPayAmounts = _shopContext.Orders.Where(x => x.IsPayed).Sum(x => x.PayAmount);
            var report = new ReportCountViewModel(orderCount, userCount, productCount, orderPayAmounts.ToMoney());
            return report;
        }
    }

    public class ProductReportHelper
    {
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public long Count { get; set; }
    }
}
