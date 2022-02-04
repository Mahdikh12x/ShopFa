using _01_ShopFaQuery.Contracts.ProductCategory;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents.ProductCategoryWithProductsViewComponents
{
    public class ProductCategoryWithProducts:ViewComponent
    {
        private readonly IProductCategoryQuery _query;

        public ProductCategoryWithProducts(IProductCategoryQuery query)
        {
            _query = query;
        }

        public IViewComponentResult Invoke()
        {
            var categories = _query.GetCategoriesWhitProducts();
            return View(categories);
        }
    }
}
