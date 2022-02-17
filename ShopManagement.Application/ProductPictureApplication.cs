using _0_Framework.Application;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductPictureAgg;

namespace ShopManagement.Application
{
    public class ProductPictureApplication : IProductPictureApplication
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductPictureRepository _productPictureRepository;
        private readonly IFileUploader _fileUploader;
        public ProductPictureApplication(IProductPictureRepository productPictureRepository, IFileUploader fileUploader, IProductRepository productRepository)
        {
            _productPictureRepository = productPictureRepository;
            _fileUploader = fileUploader;
            _productRepository = productRepository;
        }

        public OperationResult Create(CreateProductPicture command)
        {
            var operation = new OperationResult();
            var productWithCategory = _productRepository.GetProductWithCategory(command.ProductId);
            var path = $"{productWithCategory.ProductCategory.Slug}/{productWithCategory.Slug}";
            var picturePath = _fileUploader.Upload(command.Picture, path);
            var productPicture = new ProductPicture(command.ProductId, picturePath, command.PictureAlt,
                command.PictureTitle);
            _productPictureRepository.Create(productPicture);
            _productPictureRepository.SaveChanges();
            return operation.Succedded();

        }

        public OperationResult Edit(EditProductPicture command)
        {
            var operation = new OperationResult();
            var productPicture = _productPictureRepository.GetProductWithCategory(command.Id);
            if (productPicture == null)
                return operation.Failed(ApplicationValidationMessages.NotExisted);

            var path = $"{productPicture.Product.ProductCategory.Slug}/{productPicture.Product.Slug}";

            var picturePath = _fileUploader.Upload(command.Picture, path);

            productPicture.Edit(command.ProductId, picturePath, command.PictureAlt, command.PictureTitle);
            _productPictureRepository.SaveChanges();
            return operation.Succedded();
        }

        public OperationResult Remove(long id)
        {
            var operation = new OperationResult();
            var productPicture = _productPictureRepository.Get(id);
            if (productPicture == null) return operation.Failed(ApplicationValidationMessages.NotExisted);
            productPicture.Remove();
            _productPictureRepository.SaveChanges();
            return operation.Succedded();
        }

        public OperationResult Restore(long id)
        {
            var operation = new OperationResult();
            var productPicture = _productPictureRepository.Get(id);
            if (productPicture == null) return operation.Failed(ApplicationValidationMessages.NotExisted);
            productPicture.Restore();
            _productPictureRepository.SaveChanges();
            return operation.Succedded();

        }

        public List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel)
        {
            return _productPictureRepository.Search(searchModel);
        }

        public EditProductPicture GetDetails(long id)
        {
           return _productPictureRepository.GetDetails(id);
        }
    }
}
