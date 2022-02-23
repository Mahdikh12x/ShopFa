using BlogManagement.Application.Contracts.Article;
using BlogManagement.Application.Contracts.ArticleCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.administration.Pages.Blog.Articles
{
    public class EditModel : PageModel
    {
        private readonly IArticleApplication _articleApplication;
        private readonly IArticleCategoryApplication _categoryApplication;
        public EditArticle Command;
        public SelectList Categories;
        public EditModel(IArticleApplication application, IArticleCategoryApplication categoryApplication)
        {
            _articleApplication = application;
            _categoryApplication = categoryApplication;
        }

        public void OnGet(long id)
        {
            Categories = new SelectList(_categoryApplication.GetArticleCategoryNames(), "Id", "Name");
            Command= _articleApplication.GetDetails(id);
        }

        public IActionResult OnPost(EditArticle command)
        {
            var result = _articleApplication.Edit(command);
            return RedirectToPage("./Index");
        }
    }
}
