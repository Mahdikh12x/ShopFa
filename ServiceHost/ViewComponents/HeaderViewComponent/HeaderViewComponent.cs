using _01_ShopFaQuery.Contracts.ArticleCategory;
using _01_ShopFaQuery.Contracts.Menu;
using _01_ShopFaQuery.Contracts.ProductCategory;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents.HeaderViewComponent
{
    public class HeaderViewComponent:ViewComponent
    {
        private readonly IArticleCategoryQuery _articleCategory;
        private readonly IProductCategoryQuery _productCategory;
        public HeaderViewComponent(IArticleCategoryQuery articleCategory, IProductCategoryQuery productCategory)
        {
            _articleCategory = articleCategory;
            _productCategory = productCategory;
        }

        public IViewComponentResult Invoke()
        {
            var result = new MenuModel
            {
                ProductCategories = _productCategory.GetList(),
                ArticleCategories = _articleCategory.GetArticleCategoryList()
            };
            return View(result);
        }
    }
}
