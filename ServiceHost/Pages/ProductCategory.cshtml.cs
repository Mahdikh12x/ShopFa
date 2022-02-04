using _01_ShopFaQuery.Contracts.Product;
using _01_ShopFaQuery.Contracts.ProductCategory;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ProductCategoryModel : PageModel
    {
        private readonly IProductCategoryQuery _productCategoryQuery;
        public List<ProductQueryModel> Products;
        public ProductCategoryModel(IProductCategoryQuery productCategoryQuery)
        {
            _productCategoryQuery = productCategoryQuery;
        }

        public void OnGet(string id)
        {
            Products = _productCategoryQuery.GetProductsCategoryBy(id);
            
        }
    }
}
