using _0_Framework.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ServiceHost.Pages;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.administration.Pages.Shop.ProductCategories
{
    public class IndexModel : PageModel
    {
        public List<ProductCategoryViewModel>? ProductCategories;
        public ProductCategorySearchModel? SearchModel;
        private readonly IProductCategoryApplication _productCategoryApplication;
        public IndexModel(IProductCategoryApplication productCategoryApplication, IFileUploader fileUploader)
        {
            _productCategoryApplication = productCategoryApplication;
        }

        public async Task OnGet(ProductCategorySearchModel searchModel)
        {
            ProductCategories = await _productCategoryApplication.SearchAsync(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            return Partial("./Create");
        }
        public JsonResult OnPostCreate(CreateProductCategory command)
        {
            var result = _productCategoryApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var productCategory = _productCategoryApplication.GetDetails(id);
            return Partial("./Edit", productCategory);
        }
        public IActionResult OnPostEdit(EditProductCategory command)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage("./index");
            }

            var result = _productCategoryApplication.Edit(command);

            return new JsonResult(result);
        }

    }
}
