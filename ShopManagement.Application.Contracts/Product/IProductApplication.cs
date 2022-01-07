using _0_Framework.Application;

namespace ShopManagement.Application.Contracts.Product
{
    public interface IProductApplication
    {
        OperationResult Create(CreateProduct command);
        OperationResult Edit(EditProduct command);
        OperationResult NotInStock(long id);
        OperationResult InStock(long id);

        List<ProductViewModel> GetProducts();
        List<ProductViewModel> Search(ProductSearchModel searchModel);
        EditProduct GetDetails(long id);
    }
}
