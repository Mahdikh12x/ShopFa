using ShopManagement.Application.Contracts.Order;

namespace _01_ShopFaQuery.Contracts.Product
{
    public interface IProductQuery
    {
        List<ProductQueryModel> GetArrivalsProducts();
        List<ProductQueryModel> SearchProduct(string value);
        ProductQueryModel? GetProduct(string slug);
        List<CartItem>? CheckCartProductItems(List<CartItem>? cartItems);

    }
}
