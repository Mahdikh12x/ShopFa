using ShopManagement.Application.Contracts.Order;

namespace _01_ShopFaQuery.Contracts.Order;

public interface ICartService
{
    Cart ComputeCart(List<CartItem>?cartItems);
}