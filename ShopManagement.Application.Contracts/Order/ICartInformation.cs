namespace ShopManagement.Application.Contracts.Order
{
    public interface ICartInformation
    {
        void Set(Cart cart);
        Cart Get();
    }
}
