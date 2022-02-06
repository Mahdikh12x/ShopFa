using _01_ShopFaQuery.Contracts.Product;

namespace _01_ShopFaQuery.Contracts.ProductCategory;

public interface IProductCategoryQuery
{
    List<ProductCategoryQueryModel> GetList();
    List<ProductCategoryQueryModel> GetCategoriesWhitProducts();

    ProductCategoryQueryModel GetProductsCategoryBy(string slug);
}