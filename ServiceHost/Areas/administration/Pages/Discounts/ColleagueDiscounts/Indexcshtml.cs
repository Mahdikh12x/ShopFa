using DiscountManagement.Application.Contracts.ColleagueDiscount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;

namespace ServiceHost.Areas.administration.Pages.Discounts.ColleagueDiscounts;

public class IndexModel : PageModel
{
    public List<ColleagueDiscountViewModel> Discounts;
    public ColleagueDiscountSearchModel SearchModel;
    public SelectList Products;
    private readonly IProductApplication _productApplication;
    private readonly IColleagueDiscountApplication _discountApplication;
    public IndexModel(IProductApplication productApplication, IColleagueDiscountApplication discountApplication)
    {
        _productApplication = productApplication;
        _discountApplication = discountApplication;
    }

    public void OnGet(ColleagueDiscountSearchModel searchModel)
    {
        Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
        Discounts = _discountApplication.Search(searchModel);
    }

    public IActionResult OnGetCreate()
    {
        var command = new DefineColleagueDiscount
        {
            Products = _productApplication.GetProducts()
        };
        return Partial("./Create", command);
    }

    public JsonResult OnPostCreate(DefineColleagueDiscount command)
    {
        var result = _discountApplication.Define(command);
        return new JsonResult(result);
    }

    public IActionResult OnGetEdit(long id)
    {
        var customerDiscount = _discountApplication.GetDetails(id);
        if (customerDiscount != null) customerDiscount.Products = _productApplication.GetProducts();

        return Partial("./Edit", customerDiscount);
    }
    public JsonResult OnPostEdit(EditColleagueDiscount command)
    {
        var result = _discountApplication.Edit(command);
        return new JsonResult(result);
    }

    public RedirectToPageResult OnGetActive(long id)
    {
        _discountApplication.Active(id);
        return RedirectToPage();
    }
    public RedirectToPageResult OnGetDeActive(long id)
    {
        _discountApplication.DeActive(id);
        return RedirectToPage();
    }
}
