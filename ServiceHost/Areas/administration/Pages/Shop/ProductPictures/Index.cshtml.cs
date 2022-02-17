using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductPicture;

namespace ServiceHost.Areas.administration.Pages.Shop.ProductPictures
{
    public class IndexModel : PageModel
    {
        public List<ProductPictureViewModel>? ProductPictures;
        public ProductPictureSearchModel? SearchModel;
        public SelectList? Products;
        private readonly IProductPictureApplication _productPictureApplication;
        private readonly IProductApplication _productApplication;
        public IndexModel(IProductPictureApplication productPictureApplication, IProductApplication productApplication)
        {
            _productPictureApplication = productPictureApplication;
            _productApplication = productApplication;
        }

        public void OnGet(ProductPictureSearchModel searchModel)
        {
            ProductPictures = _productPictureApplication.Search(searchModel);
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
        }

        public IActionResult OnGetRemove(long id)
        {
            var result = _productPictureApplication.Remove(id);
            return RedirectToPage("./Index", result);

        }
        public IActionResult OnGetRestore(long id)
        {
            var result = _productPictureApplication.Restore(id);
            return RedirectToPage("./Index", result);

        }

        public PartialViewResult OnGetCreate()
        {
            var command = new CreateProductPicture
            {
                Products = _productApplication.GetProducts()
            };
            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(CreateProductPicture command)
        {
            var result = _productPictureApplication.Create(command);
            return new JsonResult(result);
        }

        public PartialViewResult OnGetEdit(long id)
        {
            var result = _productPictureApplication.GetDetails(id);
            result.Products = _productApplication.GetProducts();
            return Partial("./Edit", result);
        }

        public JsonResult OnPostEdit(EditProductPicture command)
        {
            var result = _productPictureApplication.Edit(command);
            return new JsonResult(result);
        }
    }
}
