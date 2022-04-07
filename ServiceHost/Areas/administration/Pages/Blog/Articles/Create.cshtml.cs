using BlogManagement.Application.Contracts.Article;
using BlogManagement.Application.Contracts.ArticleCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.administration.Pages.Blog.Articles
{
    public class CreateModel : PageModel
    {
        public CreateArticle Command;
        public SelectList Categories;
        private readonly IArticleApplication _articleApplication;
        private readonly IArticleCategoryApplication _categoryApplication;

        public CreateModel(IArticleApplication articleApplication, IArticleCategoryApplication categoryApplication)
        {
            _articleApplication = articleApplication;
            _categoryApplication = categoryApplication;
        }

        public void OnGet()
        {
            Categories = new SelectList(_categoryApplication.GetArticleCategoryNames(), "Id", "Name");
        }

        public IActionResult OnPost(CreateArticle command)
        {
            var result = _articleApplication.Create(command);
            return RedirectToPage("./Index");
        }
    }
}
