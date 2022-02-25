using _01_ShopFaQuery.Contracts.Article;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents.RecentPostsViewComponent
{
    public class RecentPostViewComponent:ViewComponent
    {
        private readonly IArticleQuery _query;

        public RecentPostViewComponent(IArticleQuery query)
        {
            _query = query;
        }

        public IViewComponentResult Invoke()
        {
            var result = _query.GetRecentPosts();
            return View(result);
        }
    }
}
