using _0_Framework.Application;
using _0_Framework.Infrastructure;
using CommentManagement.Application.Contract.Comment;
using CommentManagement.Domain.CommentAgg;
using Microsoft.EntityFrameworkCore;

namespace CommentManagement.Infrastructure.EFCore.Repository
{
    public class CommentRepository:BaseRepository<long,Comment>,ICommentRepository
    {
        private readonly CommentContext _context;
        public CommentRepository(CommentContext context):base(context)
        {
            _context = context;
        }


        public List<CommentViewModel> Search(CommentSearchModel searchModel)
        {
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
                
            }).ToList();

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(x => x.Name.Contains(searchModel.Name)).ToList();

            if (searchModel.Type != 0)
                query = query.Where(x => x.Type == searchModel.Type).ToList();

            return query.OrderByDescending(x=>x.Id).ToList();
        }
    }
}
