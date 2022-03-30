using _0_Framework.Application;
using _0_Framework.Infrastructure;
using BlogManagement.Infrastructure.EfCore;
using CommentManagement.Application.Contract.Comment;
using CommentManagement.Domain.CommentAgg;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infrastructure.EFCore;

namespace CommentManagement.Infrastructure.EFCore.Repository
{
    public class CommentRepository:BaseRepository<long,Comment>,ICommentRepository
    {
        private readonly CommentContext _context;
        private readonly ShopContext _shopContext;
        private readonly BlogContext _blogContext;

        public CommentRepository(CommentContext context, ShopContext shopContext, BlogContext blogContext):base(context)
        {
            _context = context;
            _shopContext = shopContext;
            _blogContext = blogContext;
        }


        public List<CommentViewModel> Search(CommentSearchModel searchModel)
        {
            var products = _shopContext.Products.Select(x => new { x.Id, x.Name }).ToList();
            var articles = _blogContext.Articles.Select(x => new { x.Id, x.Title }).ToList();
            var query = _context.Comments
                .Select(x => new CommentViewModel
            {
                Id = x.Id,
                Description = x.Description,
                Email = x.Email,
                Name = x.Name,
                //Parent = x.Parent.Name,
                ParentId = x.ParentId,
                Type = x.Type,
                OwnerRecordId = x.OwnerRecordId,
                CreationDate = x.CreationDate.ToFarsi(),
                IsCanceled = x.IsCanceled,
                IsConfirmed = x.IsConfirmed,
            }).OrderByDescending(x=>x.Id).ToList();

            foreach (var comment in query)
            {
                comment.Owner = comment.Type switch
                {
                    CommentType.Product => products.FirstOrDefault(x => x.Id == comment.OwnerRecordId)?.Name!,
                    CommentType.Article => articles.FirstOrDefault(x => x.Id == comment.OwnerRecordId)?.Title!,
                    _ => comment.Owner
                };
            }

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(x => x.Name.Contains(searchModel.Name)).ToList();

            if (searchModel.Type != 0)
                query = query.Where(x => x.Type == searchModel.Type).ToList();


            if (searchModel.Pending)
                query = query.Where(x => !x.IsConfirmed && !x.IsCanceled).ToList();
            return query.ToList();
        }
    }
}
