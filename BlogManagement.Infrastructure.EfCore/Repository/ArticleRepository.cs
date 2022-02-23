using System.Globalization;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using BlogManagement.Application.Contracts.Article;
using BlogManagement.Domain.ArticleAgg;
using Microsoft.EntityFrameworkCore;

namespace BlogManagement.Infrastructure.EfCore.Repository;

public class ArticleRepository : BaseRepository<long, Article>, IArticleRepository
{
    private readonly BlogContext _context;

    public ArticleRepository(BlogContext context) : base(context)
    {
        _context = context;
    }

    public EditArticle? GetDetails(long id)
    {
        return _context.Articles.Select(x => new EditArticle
        {
            Id = x.Id,
            Body = x.Body,
            Title = x.Title,
            ShortDescription = x.ShortDescription,
            PublishDate = x.PublishDate.ToString(CultureInfo.InvariantCulture),
            CanonicalAddress = x.CanonicalAddress,
            Slug = x.Slug,
            PictureTitle = x.PictureTitle,
            CategoryId = x.CategoryId,
            MetaDescription = x.MetaDescription,
            PictureAlt = x.PictureAlt,
            Keywords = x.Keywords,
        }).FirstOrDefault(x => x.Id == id);
    }

    public List<ArticleViewModel> Search(ArticleSearchModel searchModel)
    {
        var query = _context.Articles.Include(x => x.Category).Select(x => new ArticleViewModel
        {

            Title = x.Title,
            Picture = x.Picture,
            PublishDate = x.PublishDate.ToFarsi(),
            IsActivate = x.IsActive,
            CategoryId = x.CategoryId,
            Category = x.Category.Name,
            Id = x.Id
        });

        if (!string.IsNullOrWhiteSpace(searchModel.Title))
            query = query.Where(x => x.Title.Contains(searchModel.Title));

        if (searchModel.CategoryId > 0)
            query = query.Where(x => x.CategoryId == searchModel.CategoryId);

        if (searchModel.IsActive)
            query = query.Where(x => x.IsActivate != searchModel.IsActive);

        return query.OrderByDescending(x => x.Id).ToList();
    }

    public Article? GetArticleWithCategory(long id)
    {
        return _context.Articles.Include(x => x.Category).FirstOrDefault(x => x.Id == id);
    }
}