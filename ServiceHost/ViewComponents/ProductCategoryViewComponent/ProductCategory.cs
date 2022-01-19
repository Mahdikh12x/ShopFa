using _01_ShopFaQuery.Contracts.ProductCategory;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents.ProductCategoryViewComponent
{

    public class ProductCategory:ViewComponent
    {
        private readonly IProductCategoryQuery _productCategory;

        public ProductCategory(IProductCategoryQuery productCategory)
        {
            _productCategory = productCategory;
        }

        public IViewComponentResult Invoke()
        {
            var productCategory = _productCategory.GetList();
            return View(productCategory);
        }
    }
}
