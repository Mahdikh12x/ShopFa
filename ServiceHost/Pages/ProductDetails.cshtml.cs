using _0_Framework.Infrastructure;
using _01_ShopFaQuery.Contracts.Product;
using CommentManagement.Application.Contract.Comment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ProductDetailsModel : PageModel
    {
        private readonly IProductQuery _productQuery;
        private readonly ICommentApplication _commentApplication;
        public ProductQueryModel? Product;

        public ProductDetailsModel(IProductQuery productQuery, ICommentApplication commentApplication)
        {
            _productQuery = productQuery;
            _commentApplication = commentApplication;
        }

        public void OnGet(string id)
        {
            Product = _productQuery.GetProduct(id);
        }

        public RedirectToPageResult OnPost(AddComment command,string productSlug)
        {
            command.Type = CommentType.Product;
            _commentApplication.AddComment(command);
            return RedirectToPage("./ProductDetails",new{Id= productSlug});
        }
    }
}
