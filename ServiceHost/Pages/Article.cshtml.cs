using _0_Framework.Infrastructure;
using _01_ShopFaQuery.Contracts.Article;
using CommentManagement.Application.Contract.Comment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ArticleModel : PageModel
    {
        public ArticleQueryModel? Article;
        private readonly IArticleQuery _articleQuery;
        private readonly ICommentApplication _commentApplication;
        public ArticleModel(IArticleQuery articleQuery, ICommentApplication commentApplication)
        {
            _articleQuery = articleQuery;
            _commentApplication = commentApplication;
        }

        public void OnGet(string id)
        {
            Article = _articleQuery.GetArticle(id);
        }

        public RedirectToPageResult OnPost(AddComment comment,string articleSlug)
        {
            comment.Type=CommentType.Article;
            _commentApplication.AddComment(comment);

            return RedirectToPage("/Article", new { Id = articleSlug });
        }
    }
}
