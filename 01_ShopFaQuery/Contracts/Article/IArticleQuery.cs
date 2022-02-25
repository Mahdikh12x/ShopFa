namespace _01_ShopFaQuery.Contracts.Article
{
    public interface  IArticleQuery
    {
        List<ArticleQueryModel> GetLastArticles();
        List<ArticleQueryModel> GetRecentPosts();
        List<ArticleQueryModel> SearchArticles(string search);
        ArticleQueryModel? GetArticle(string slug);

    }
}
