using _0_Framework.Application;

namespace ShopManagement.Application.Contracts.ProductCategory
{
    public interface IProductCategoryApplication
    {
        OperationResult Create(CreateProductCategory command);
        Task<List<ProductCategoryViewModel>?> GetProductCategoriesAsync();

        OperationResult Edit(EditProductCategory command);
        EditProductCategory GetDetails(long id);
        Task<List<ProductCategoryViewModel>?> SearchAsync(ProductCategorySearchModel searchModel);
    }
}