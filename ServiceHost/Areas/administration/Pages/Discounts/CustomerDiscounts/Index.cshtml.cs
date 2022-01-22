using DiscountManagement.Application.Contracts.CustomerDiscount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.administration.Pages.Discounts.CustomerDiscounts
{
    public class IndexModel : PageModel
    {
        public List<CustomerDiscountViewModel> Discounts;
        public CustomerDiscountSearch SearchModel;
        public SelectList Products;
        private readonly IProductApplication _productApplication;
        private readonly ICustomerDiscountApplication _discountApplication;
        public IndexModel(IProductApplication productApplication, ICustomerDiscountApplication discountApplication)
        {
            _productApplication = productApplication;
            _discountApplication = discountApplication;
        }

        public void OnGet(CustomerDiscountSearch searchModel)
        {
            Products= new SelectList(_productApplication.GetProducts(), "Id", "Name");
            Discounts= _discountApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            var command = new DefineCustomerDiscount
            {
               Products = _productApplication.GetProducts()
            };
            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(DefineCustomerDiscount command)
        {
            var result = _discountApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var customerDiscount= _discountApplication.GetDetails(id);
            if (customerDiscount != null) customerDiscount.Products = _productApplication.GetProducts();

            return Partial("./Edit", customerDiscount);
        }
        public JsonResult OnPostEdit(EditCustomerDiscount command)
        {
            var result = _discountApplication.Edit(command);
            return new JsonResult(result);
        }

    }
}
