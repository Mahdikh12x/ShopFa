using _0_Framework.Domain;
using ShopManagement.Application.Contracts.ProductPicture;

namespace ShopManagement.Domain.ProductPictureAgg
{
    public interface IProductPictureRepository:IRepository<long,ProductPicture>
    {
        List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel);

        EditProductPicture GetDetails(long id);
    }

}
