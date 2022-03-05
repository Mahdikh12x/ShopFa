using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using CommentManagement.Application.Contract.Comment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.administration.Pages.Comment
{
    public class IndexModel : PageModel
    {
        public List<CommentViewModel> Comments;
        public CommentSearchModel SearchModel;
        internal enum CommentTypes
        {
            [Display(Name = "محصولات")]
            Product =1,
            [Display(Name = "مقالات")]
            Article= 2
        }
        private readonly ICommentApplication _commentApplication;
        public IndexModel(ICommentApplication commentApplication)
        {
            _commentApplication = commentApplication;
        }
        public void OnGet(CommentSearchModel searchModel)
        {
            Comments = _commentApplication.Search(searchModel);
        }
        public RedirectToPageResult OnGetConfirm(long id)
        {
            var result=_commentApplication.Confirm(id);
           return RedirectToPage();
        } 
        public RedirectToPageResult OnGetCancel(long id)
        {
            var result=_commentApplication.Cancel(id);
           return RedirectToPage();
        }
    }
}
