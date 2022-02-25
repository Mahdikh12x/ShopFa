using _01_ShopFaQuery.Contracts.Article;
using _01_ShopFaQuery.Contracts.Product;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class SearchModel : PageModel
    {
        public string Value;
        public List<ProductQueryModel>? Products;
        public List<ArticleQueryModel>? Articles;
        private readonly IArticleQuery _articleQuery;
        private readonly IProductQuery _productQuery;
        public SearchModel(IProductQuery productQuery, IArticleQuery articleQuery)
        {
            _productQuery = productQuery;
            _articleQuery = articleQuery;
        }

        public void OnGet(string value)
        {
            Value = value;
            Products = _productQuery.SearchProduct(value);
            Articles = _articleQuery.SearchArticles(value);
        }
    }
}
