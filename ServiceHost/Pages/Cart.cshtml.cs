using System.Net;
using _01_ShopFaQuery.Contracts.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nancy.Json;
using ShopManagement.Application.Contracts.Order;

namespace ServiceHost.Pages
{
    public class CartModel : PageModel
    {
        public List<CartItem>? Items;
        public const string CookieName = "basket-item";
        private readonly IProductQuery _productQuery;

        public CartModel(IProductQuery productQuery)
        {
            _productQuery = productQuery;
            Items = new List<CartItem>();
        }

        public void OnGet()
        {
            var serializer = new JavaScriptSerializer();
            var value = Request.Cookies[CookieName];
            var cartItems = serializer.Deserialize<List<CartItem>>(value);
            if (cartItems != null)
            {
                if (cartItems.Count == 0)
                {
                    Response.Cookies.Delete(CookieName);
                }

                foreach (var item in cartItems)
                {
                   item.CalculateTotalPrice();
                }
                Items = _productQuery.CheckCartProductItems(cartItems);
            }

        }


        public IActionResult OnGetRemoveFromCart(long id)
        {
            var serializer = new JavaScriptSerializer();
            var value = Request.Cookies[CookieName];
            var cartItems = serializer.Deserialize<List<CartItem>>(value);
            Response.Cookies.Delete(CookieName);
            var currentProduct = cartItems.FirstOrDefault(item => item.Id == id);
            if (currentProduct != null)
                cartItems.Remove(currentProduct);
            var options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(2),
                Path = "/",
                //HttpOnly = true,
                IsEssential = true
            };
            Response.Cookies.Append(CookieName, serializer.Serialize(cartItems), options);
            return RedirectToPage("/Cart");
        }

        public IActionResult OnGetCheckout()
        {
            var serializer= new JavaScriptSerializer();
            var value= Request.Cookies[CookieName];
            var cartItems = serializer.Deserialize<List<CartItem>>(value);
            foreach (var item in cartItems)
            {
                item.CalculateTotalPrice();
            }
            Items = _productQuery.CheckCartProductItems(cartItems);
            return RedirectToPage(Items != null && Items.Any(x => !x.IsInStock) ? "Cart" : "Checkout");
        }
    }
}
