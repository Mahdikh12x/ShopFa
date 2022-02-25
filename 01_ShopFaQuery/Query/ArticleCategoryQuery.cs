using _01_ShopFaQuery.Contracts.Article;
using _01_ShopFaQuery.Contracts.ArticleCategory;
using BlogManagement.Domain.ArticleAgg;
using BlogManagement.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;

namespace _01_ShopFaQuery.Query
{
    public class ArticleCategoryQuery:IArticleCategoryQuery
    {
        private readonly BlogContext _context;

        public ArticleCategoryQuery(BlogContext context)
        {
            _context = context;
        }

        public ArticleCategoryQueryModel? GetArticleCategory(string slug)
        {
            var articleCategories= _context.ArticleCategories.Include(x=>x.Articles).Select(x => new ArticleCategoryQueryModel
                {
                    Name = x.Name,
                    Slug = x.Slug,
                    MetaDescription = x.MetaDescription,
                    Articles = MapArticles(x.Articles),
                    CanonicalAddress = x.CanonicalAddress,
                    Keywords = x.Keywords
                })
                .FirstOrDefault(x => x.Slug == slug);

            if (!string.IsNullOrWhiteSpace(articleCategories?.Keywords))
                articleCategories.KeywordsList = articleCategories.Keywords.Split(new Char[] { ',', '،' },StringSplitOptions.RemoveEmptyEntries).ToList();
            return articleCategories;
        }

        public List<ArticleCategoryQueryModel> GetArticleCategoryList()
        {
            return _context.ArticleCategories.Include(x=>x.Articles).Select(x => new ArticleCategoryQueryModel
            {
             
                Name = x.Name,
                Slug = x.Slug,
                ArticleCount = x.Articles.Count,
                
            }).ToList();
        }

        private static List<ArticleQueryModel> MapArticles(IEnumerable<Article> articles)
        {
            var list = articles.Select(x => new ArticleQueryModel
            {
                Id = x.Id,
                PictureAlt = x.PictureAlt,
                CategoryId = x.CategoryId,
                Slug = x.Slug,
                Title = x.Title,
                ShortDescription = x.ShortDescription,
                PictureTitle = x.PictureTitle,
                Picture = x.Picture,
            }).ToList();
            return list;
        }
    }
}

