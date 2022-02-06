using _01_ShopFaQuery.Contracts.Product;
using _01_ShopFaQuery.Contracts.ProductCategory;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ProductCategoryModel : PageModel
    {
        private readonly IProductCategoryQuery _productCategoryQuery;
        public ProductCategoryQueryModel ProductCategory;
        public ProductCategoryModel(IProductCategoryQuery productCategoryQuery)
        {
            _productCategoryQuery = productCategoryQuery;
        }

        public void OnGet(string id)
        {
            ProductCategory = _productCategoryQuery.GetProductsCategoryBy(id);
            
        }
    }
}
