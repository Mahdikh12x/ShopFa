using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AccountManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Domain.OrderAgg;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class OrderRepository : BaseRepository<long, Order>, IOrderRepository
    {
        private readonly ShopContext _shopContext;
        private readonly AccountContext _accountContext;
        public OrderRepository(ShopContext context, AccountContext accountContext) : base(context)
        {
            _shopContext = context;
            _accountContext = accountContext;
        }

        public double GetPayAmountBy(long orderId)
        {
            return _shopContext.Orders
                !.Select(order => new { order.Id, order.PayAmount })
                .FirstOrDefault(x => x.Id == orderId)!.PayAmount;
        }

        public List<OrderViewModel> Search(OrderSearchModel searchModel)
        {
            var accounts = _accountContext.Users.Select(x => new { x.Id, x.Fullname }).AsNoTracking().ToList();
            var query = _shopContext.Orders?.Select(order => new OrderViewModel
            {
                Id = order.Id,
                AccountId = order.AccountId,
                TotalAmount = order.TotalAmount.ToString("n0"),
                RefId = order.RefId,
                IssuesTrackingNum = order.IssuesTrackingNum,
                DiscountAmount = order.DiscountAmount.ToString("n0"),
                IsPayed = order.IsPayed,
                PayAmount = order.PayAmount.ToString("n0"),
                CreationDate = order.CreationDate.ToFarsi(),
                IsCanceled = order.IsCanceled,
                PaymentMethodId = order.PaymentMethod,
            });

            if (searchModel.RefId > 0)
                query = query.Where(x => x.RefId == searchModel.RefId);
            if (searchModel.IsCanceled)
                query = query.Where(x => x.IsCanceled);

            if (searchModel.IsPayed)
                query = query.Where(x => x.IsPayed);
            var orders = query.OrderByDescending(x => x.Id).ToList();
            foreach (var order in orders)
            {
                order.AccountName = accounts.FirstOrDefault(x => x.Id == order.AccountId)?.Fullname;
                order.PaymentMethod = PaymentMethod.GetMethodBy(order.PaymentMethodId)?.Name;
            }

            if (!string.IsNullOrWhiteSpace(searchModel.AccountName))
                orders = orders.Where(x => x.AccountName != null && x.AccountName.Contains(searchModel.AccountName)).ToList();

            return orders;

        }

        public List<OrderItemViewModel> GetItems(long id)
        {
            var order = _shopContext.Orders.Find(id);
            var products = _shopContext.Products.Select(x => new { x.Id, x.Name }).AsNoTracking().ToList();
            var items = order?.Items.Select(x => new OrderItemViewModel
            {
                Id = x.Id,
                OrderId = x.OrderId,
                DiscountRate = x.DiscountRate,
                Count = x.Count,
                ProductId = x.ProductId,
                UnitePrice = x.UnitPrice.ToString("n0"),
            }).ToList();
            if (items == null) return new List<OrderItemViewModel>();
            {
                foreach (var orderItem in items)
                {
                    orderItem.ProductName = products.FirstOrDefault(x => x.Id == orderItem.ProductId)?.Name;
                }
                return items;
            }

        }
    }
}
