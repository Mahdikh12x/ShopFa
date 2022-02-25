using _01_ShopFaQuery.Contracts.ArticleCategory;
using _01_ShopFaQuery.Contracts.ProductCategory;

namespace _01_ShopFaQuery.Contracts.Menu
{
    public class MenuModel
    {
        public List<ArticleCategoryQueryModel>? ArticleCategories { get; set; }
        public List<ProductCategoryQueryModel>? ProductCategories { get; set; }
    }
}
