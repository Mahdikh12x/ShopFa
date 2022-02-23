using _0_Framework.Application;
using _0_Framework.Infrastructure;
using BlogManagement.Application.Contracts.ArticleCategory;
using BlogManagement.Domain.ArticleCategoryAgg;

namespace BlogManagement.Infrastructure.EfCore.Repository
{
    public class ArticleCategoryRepository:BaseRepository<long,ArticleCategory>,IArticleCategoryRepository
    {
        private readonly BlogContext _context;
        public ArticleCategoryRepository(BlogContext context):base(context)
        {
            _context = context;
        }

        public EditArticleCategory? GetDetails(long id)
        {
            return _context.ArticleCategories.Select(x=>new EditArticleCategory()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                ShowOrder = x.ShowOrder,
                Slug = x.Slug,
                CanonicalAddress = x.CanonicalAddress,
                Keywords = x.Keywords,
                PictureTitle = x.PictureTitle,
                PictureAlt = x.PictureAlt,
                MetaDescription = x.MetaDescription

            }).FirstOrDefault(x=>x.Id==id);
        }

        public List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel)
        {
            var query = _context.ArticleCategories.Select(x => new ArticleCategoryViewModel
            {
                Id = x.Id,
                Description = x.Description,
                Name = x.Name,
                ShowOrder = x.ShowOrder,
                CreationDate = x.CreationDate.ToFarsi(),
                 Picture = x.Picture
            });

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(x => x.Name.Contains(searchModel.Name));

            return query.OrderByDescending(x => x.Id).ToList();

        }

        public List<ArticleCategoryViewModel> GetArticleCategoryNames()
        {
            return _context.ArticleCategories.Select(x => new ArticleCategoryViewModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
        }

        public string GetArticleCategorySlug(long id)
        {
            return _context.ArticleCategories.Select(x => new { x.Id, x.Slug }).FirstOrDefault(x => x.Id == id)!.Slug;

        }
    }
}
