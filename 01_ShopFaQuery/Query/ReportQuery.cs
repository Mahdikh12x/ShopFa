using System.Collections.ObjectModel;
using _0_Framework.Application;
using _01_ShopFaQuery.Contracts.Report;
using AccountManagement.Infrastructure.EFCore;
using InventoryManagement.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;
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

        public Collection<List<ChartViewModel>> GetReports()
        {
            var orders = _shopContext.Orders.Where(x => x.CreationDate>DateTime.Now.AddDays(-30)).ToList();
            var chartList=new Collection<List<ChartViewModel>>();

            var waitingPayChart=orders.Where(x=>!x.IsCanceled&&!x.IsPayed).Select(x => new ChartViewModel
            {
                Label = "در انتظار پرداخت",
                Data = orders.Count(x => !x.IsCanceled && !x.IsPayed)
            }).ToList();
            chartList.Add(waitingPayChart);

            var PayChart = orders.Where(x=>x.IsPayed).Select(x => new ChartViewModel
            {
                Label = "پرداخت شده",
                Data = orders.Count(x => x.IsPayed),
                
            }).ToList();
            chartList.Add(PayChart);

            return chartList;
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
