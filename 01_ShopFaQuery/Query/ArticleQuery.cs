using _0_Framework.Application;
using _01_ShopFaQuery.Contracts.Article;
using BlogManagement.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;

namespace _01_ShopFaQuery.Query
{
    public class ArticleQuery:IArticleQuery
    {
        private readonly BlogContext _context;

        public ArticleQuery(BlogContext context)
        {
            _context = context;
        }

        public List<ArticleQueryModel> GetLastArticles()
        {
            var articles = _context.Articles.Where(x=>x.PublishDate<DateTime.Now).Include(x=>x.Category).Select(x => new ArticleQueryModel
            {
                Id = x.Id,
                CategoryName = x.Category.Name,
                Picture = x.Picture,
                PictureTitle = x.PictureTitle,
                PictureAlt = x.PictureAlt,
                CategorySlug = x.Category.Slug,
                ShortDescription = x.ShortDescription,
                Title = x.Title,
                PublishDate = x.PublishDate.ToFarsi(),
                Slug = x.Slug,

            });

            return articles.OrderByDescending(x => x.Id).ToList();
        }

        public List<ArticleQueryModel> GetRecentPosts()
        {
            return _context.Articles.Select(x => new ArticleQueryModel
            {
                Id=x.Id,
                Title = x.Title,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                PublishDate = x.PublishDate.ToFarsi(),
                Picture = x.Picture,
                Slug = x.Slug,
                
            }).OrderByDescending(x => x.Id).ToList();
        }

        public List<ArticleQueryModel> SearchArticles(string search)
        {
            var articles = _context.Articles.Where(x => x.PublishDate < DateTime.Now).Include(x => x.Category).Select(x => new ArticleQueryModel
            {
                Id = x.Id,
                CategoryName = x.Category.Name,
                Picture = x.Picture,
                PictureTitle = x.PictureTitle,
                PictureAlt = x.PictureAlt,
                CategorySlug = x.Category.Slug,
                ShortDescription = x.ShortDescription,
                Title = x.Title,
                PublishDate = x.PublishDate.ToFarsi(),
                Slug = x.Slug,

            });

            if (!string.IsNullOrWhiteSpace(search))
                articles = articles.Where(x => x.Title.Contains(search));

            return articles.OrderByDescending(x => x.Id).ToList();
        }

        public ArticleQueryModel? GetArticle(string slug)
        {
          var article= _context.Articles.Include(x=>x.Category).Select(x => new ArticleQueryModel
            {
                Id = x.Id,
                Body = x.Body,
                Picture = x.Picture,
                Slug = x.Slug,
                Title = x.Title,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                PublishDate = x.PublishDate.ToFarsi(),
                Keywords = x.Keywords,
                ShortDescription = x.ShortDescription,
                MetaDescription = x.MetaDescription,
                CanonicalAddress = x.CanonicalAddress,
                CategoryName = x.Category.Name,
                CategorySlug = x.Category.Slug

            }).FirstOrDefault(x => x.Slug == slug);

          if (!string.IsNullOrWhiteSpace(article.Keywords))
              article.KeywordList = article.Keywords.Split(new Char[] { ',', '،' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            return article;
        }
    }
}
