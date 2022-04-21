using ShopManagement.Application.Contracts.Order;

namespace ShopManagement.Application
{
    public class CartInformation:ICartInformation
    {
        
        public Cart Cart { get; set; }

        public CartInformation()
        {
            Cart = new Cart();
        }
        public CartInformation(Cart cart)
        {
            Cart = cart;
        }

        public void Set(Cart cart)
        {
            Cart = cart;
        }

        public Cart Get()
        {
            return this.Cart;
        }
    }
}
