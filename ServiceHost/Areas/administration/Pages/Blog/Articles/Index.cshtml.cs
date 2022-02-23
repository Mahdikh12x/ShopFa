using BlogManagement.Application.Contracts.Article;
using BlogManagement.Application.Contracts.ArticleCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.administration.Pages.Blog.Articles
{
    public class IndexModel : PageModel
    {
        public ArticleSearchModel SearchModel;
        public SelectList ArticleCategories;
        private readonly IArticleApplication _articleApplication;
        public List<ArticleViewModel> Articles;
        private readonly IArticleCategoryApplication _categoryApplication;

        public IndexModel(IArticleApplication articleApplication, IArticleCategoryApplication categoryApplication)
        {
            _articleApplication = articleApplication;
            _categoryApplication = categoryApplication;
        }

        public void OnGet(ArticleSearchModel searchModel)
        {
            ArticleCategories = new SelectList(_categoryApplication.GetArticleCategoryNames(), "Id", "Name");
            Articles = _articleApplication.Search(searchModel);
        }

        public IActionResult OnGetActive(long id)
        {
            _articleApplication.Active(id);
            return RedirectToPage();


        }  public IActionResult OnGetDeActive(long id)
        { _articleApplication.DeActive(id);
            return RedirectToPage();
        }
    }
}
