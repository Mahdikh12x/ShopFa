using System.Globalization;
using _0_Framework.Application;
using _0_Framework.Application.SmsService;
using _0_Framework.Application.ZarinPalService;
using _01_ShopFaQuery.Contracts.Order;
using _01_ShopFaQuery.Contracts.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nancy.Json;
using ShopManagement.Application.Contracts.Order;

namespace ServiceHost.Pages
{


    [Authorize]
    public class CheckoutModel : PageModel
    {
        public Cart? Cart;
        public const string CookieName = "basket-item";
        private readonly ICartService _cartService;
        private readonly IProductQuery _productQuery;
        private readonly IZarinPalFactory _zarinPalFactory;
        private readonly ICartInformation _cartInformation;
        private readonly IOrderApplication _orderApplication;
        private readonly IAuthHelper _authHelper;
        public CheckoutModel(ICartService cartService, ICartInformation cartInformation, IProductQuery productQuery, IZarinPalFactory zarinPalFactory, IOrderApplication orderApplication, IAuthHelper authHelper)
        {
            _cartService = cartService;
            _cartInformation = cartInformation;
            _productQuery = productQuery;
            _zarinPalFactory = zarinPalFactory;
            _orderApplication = orderApplication;
            _authHelper = authHelper;
            Cart = new Cart();
        }

        public void OnGet()
        {
            var serializer = new JavaScriptSerializer();
            var value = Request.Cookies[CookieName];
            var cartItems = serializer.Deserialize<List<CartItem>>(value);
            foreach (var item in cartItems)
                item.CalculateTotalPrice();


            Cart = _cartService.ComputeCart(cartItems);
            _cartInformation.Set(Cart);
        }
        public async Task<IActionResult> OnPostPayment(int paymentMethod)
        {

             var cart = _cartInformation.Get();
             cart.SetPaymentMethod(paymentMethod);
            
             var validation = _productQuery.CheckCartProductItems(cart.Items);
             
             if (validation != null && validation.Any(x => !x.IsInStock))
                 RedirectToPage("Cart");
             
             var orderId = _orderApplication.PlaceOrder(cart);
             
             var currentUser = _authHelper.GetAccountInfo();
             var result = new PaymentResult();
             if (cart.PaymentMethod == TypePayMethod.OnlinePay)
             {
               
                 var paymentResponse = _zarinPalFactory.CreatePaymentRequest(
                     cart.CartPayAmount.ToString(CultureInfo.InvariantCulture), currentUser?.Mobile ?? "", "", "خرید از مجموعه ShopFa", orderId);
                 return await Task.FromResult<IActionResult>(Redirect($"https://{paymentResponse.Result.Prefix}.zarinpal.com/pg/StartPay/{paymentResponse.Result.Authority}"));
             }

             if (paymentMethod == TypePayMethod.CashPay)
             {
                 result.Succeeded(ServiceMessages.CashPaymentMessage);
                 return RedirectToPage("PaymentResult",result);
             }

             
             return RedirectToPage("/PaymentResult",result.Succeeded(ServiceMessages.InstallmentPayMessage));
             

        }


        public IActionResult OnGetCallBack([FromQuery] string authority, [FromQuery] string status, [FromQuery] long oId)
        {
            var orderAmount = _orderApplication.GetPayAmountBy(oId);
            var paymentResult =
                _zarinPalFactory.CreateVerificationRequest(authority,
                    orderAmount.ToString(CultureInfo.InvariantCulture));
            var result = new PaymentResult();
            if (paymentResult.Result.Status == 100 && status == "OK")
            {
                var issueTrackingNumber = _orderApplication.PaymentSucceeded(oId, paymentResult.Result.RefId);
                Response.Cookies.Delete(ServiceMessages.CartCookieName);
                result.Cash(issueTrackingNumber).Succeeded(ServiceMessages.SuccessfullPaymentOperation);
                return RedirectToPage("PaymentResult", result);
            }
            _orderApplication.Cancel(oId);
            result.Failed(ServiceMessages.FailedPaymentOperation);
            return RedirectToPage("PaymentResult", result);
        }
    }

}
