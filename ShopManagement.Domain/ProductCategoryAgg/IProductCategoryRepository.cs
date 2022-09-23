using _0_Framework.Domain;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ShopManagement.Domain.ProductCategoryAgg
{
    public interface IProductCategoryRepository:IRepository<long,ProductCategory>
    {
        Task<List<ProductCategoryViewModel>?> GetProductCategoriesAsync();
        EditProductCategory GetDetails(long id);
        Task<List<ProductCategoryViewModel>?> SearchAsync(ProductCategorySearchModel searchModel);
        string GetCategoryWithSlug(long id);

    }
}
