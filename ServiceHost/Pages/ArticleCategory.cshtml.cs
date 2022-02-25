using _01_ShopFaQuery.Contracts.ArticleCategory;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ArticleCategoryModel : PageModel
    {
        private readonly IArticleCategoryQuery _articleCategory;
        public ArticleCategoryQueryModel ArticleCategory;
        public ArticleCategoryModel(IArticleCategoryQuery articleCategory)
        {
            _articleCategory = articleCategory;
        }

        public void OnGet(string id)
        {
            ArticleCategory = _articleCategory.GetArticleCategory(id);
        }
    }
}
