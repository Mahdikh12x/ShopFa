using BlogManagement.Application.Contracts.ArticleCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.administration.Pages.Blog.ArticleCategories
{
    public class IndexModel : PageModel
    {
        public List<ArticleCategoryViewModel> ArticleCategories;
        public ArticleCategorySearchModel SearchModel;
        private readonly IArticleCategoryApplication _articleCategoryApplication;
        public IndexModel(IArticleCategoryApplication productCategoryApplication)
        {
            _articleCategoryApplication = productCategoryApplication;
        }

        public void OnGet(ArticleCategorySearchModel searchModel)
        {
            ArticleCategories = _articleCategoryApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {

            return Partial("./Create");
        }
        public JsonResult OnPostCreate(CreateArticleCategory command)
        {
            var result = _articleCategoryApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var productCategory = _articleCategoryApplication.GetDetails(id);
            return Partial("./Edit", productCategory);
        }
        public IActionResult OnPostEdit(EditArticleCategory command)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage("./index");
            }

            var result = _articleCategoryApplication.Edit(command);

            return new JsonResult(result);
        }

    }
}
