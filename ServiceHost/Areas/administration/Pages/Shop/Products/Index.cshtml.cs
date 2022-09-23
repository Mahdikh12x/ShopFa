using _0_Framework.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Infrastructure.Configuration.Permissions;

namespace ServiceHost.Areas.administration.Pages.Shop.Products
{
    
    public class IndexModel : PageModel
    {
        public List<ProductViewModel>? Products;
        public ProductSearchModel? SearchModel;
        private readonly IProductApplication _productApplication;
        private readonly IProductCategoryApplication _productCategoryApplication;
        public SelectList? ProductCategories;
        public IndexModel(IProductApplication productApplication, IProductCategoryApplication productCategoryApplication)
        {
            _productApplication = productApplication;
            _productCategoryApplication = productCategoryApplication;
        }

        [NeedsPermission(ShopPermissions.ShowProducts)]
        public async Task OnGet(ProductSearchModel searchModel)
        {
            ProductCategories = new SelectList(await _productCategoryApplication.GetProductCategoriesAsync(), "Id", "Name");
            Products = _productApplication.SearchAsync(searchModel)?.Result;
        }

        public  async Task<IActionResult>OnGetCreate()
        {
            var command = new CreateProduct
            {
                Categories =await _productCategoryApplication.GetProductCategoriesAsync()
            };
            return Partial("./Create", command);
        }

        [NeedsPermission(ShopPermissions.CreateProduct)]
        public IActionResult OnPostCreate(CreateProduct command)
        {
            if (ModelState.IsValid)
            {
                var result = _productApplication.Create(command);
                return new JsonResult(result);
            }
            return Page();
        }

        public async Task<IActionResult> OnGetEdit(long id)
        {
            var product = _productApplication.GetDetails(id);
            product!.Categories =await _productCategoryApplication.GetProductCategoriesAsync();
            return Partial("./Edit", product);
        }
        [NeedsPermission(ShopPermissions.EditProduct)]

        public IActionResult OnPostEdit(EditProduct command)
        {
            if (ModelState.IsValid)
            {
                var result = _productApplication.Edit(command);
                return new JsonResult(result);

            }

            return Page();
        }

      

    }
}
