using _0_Framework.Application;
using _01_ShopFaQuery.Contracts.Order;
using AccountManagement.Infrastructure.Configuration;
using DiscountManagement.Infrastructure.EFCore;
using ShopManagement.Application.Contracts.Order;

namespace _01_ShopFaQuery.Query
{
    public class CartService : ICartService
    {
        private readonly DiscountContext _discountContext;
        private readonly IAuthHelper _authHelper;
        public CartService(DiscountContext discountContext, IAuthHelper authHelper)
        {
            _discountContext = discountContext;
            _authHelper = authHelper;
        }


        public Cart ComputeCart(List<CartItem>? cartItems)
        {
            var cart = new Cart();
            var colleagueDiscounts = _discountContext.ColleagueDiscounts
                .Where(x => x.IsActivated)
                .Select(x => new { x.DiscountRate, x.ProductId }).ToList();

            var customerDiscounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new { x.DiscountRate, x.ProductId }).ToList();

            var currentRole = _authHelper.GetCurrentInfo();
                foreach (var item in cartItems)
                {
                    if (currentRole == Roles.ColleagueUser)
                    {
                        var colleague = colleagueDiscounts.FirstOrDefault(x => x.ProductId == item.Id);
                        if (colleague != null) item.DiscountRate = colleague.DiscountRate;
                    }
                    else
                    {
                        var customerDiscount = customerDiscounts.FirstOrDefault(x => x.ProductId == item.Id);
                        if (customerDiscount != null) item.DiscountRate = customerDiscount.DiscountRate;
                    }
                    item.DiscountAmount = item.DiscountRate * item.UnitPrice / 100;
                    item.ItemPayAmount = item.TotalPrice - item.DiscountAmount;
                    cart.AddToCart(item);
                }

            return cart;
        }
    }
}
