namespace _01_ShopFaQuery.Contracts.ArticleCategory
{
    public interface IArticleCategoryQuery
    {
        ArticleCategoryQueryModel? GetArticleCategory(string slug);
        List<ArticleCategoryQueryModel> GetArticleCategoryList();
    }
}
