using _01_ShopFaQuery.Contracts.Article;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents.LastArticlesViewComponent
{
    public class LastArticlesViewComponent:ViewComponent
    {
        private readonly IArticleQuery _articleQuery;
        public LastArticlesViewComponent(IArticleQuery articleQuery)
        {
            _articleQuery = articleQuery;
        }

        public IViewComponentResult Invoke()
        {
            var articles = _articleQuery.GetLastArticles();
            return View(articles);

        }
    }
}
