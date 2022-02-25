using _01_ShopFaQuery.Contracts.ArticleCategory;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents.ArticleCategoriesListViewComponent
{
    public class ArticleCategoriesListViewComponent:ViewComponent
    {
        private readonly IArticleCategoryQuery _query;

        public ArticleCategoriesListViewComponent(IArticleCategoryQuery query)
        {
            _query = query;
        }

        public IViewComponentResult Invoke()
        {
            var result = _query.GetArticleCategoryList();
            return View(result);
        }
    }
}
