using _0_Framework.Application;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Application
{
    public class ProductApplication : IProductApplication
    {
        private readonly IProductRepository _productRepository;
        private readonly IFileUploader _fileUploader;
        private readonly IProductCategoryRepository _productCategoryRepository;
        public ProductApplication(IProductRepository productRepository, IFileUploader fileUploader
            , IProductCategoryRepository productCategoryRepository)
        {
            _productRepository = productRepository;
            _fileUploader = fileUploader;
            _productCategoryRepository = productCategoryRepository;
        }

        public OperationResult Create(CreateProduct command)
        {
            var operation = new OperationResult();
            if (_productRepository.Exists(x => x.Name == command.Name))
                return operation.Failed(ApplicationValidationMessages.Duplicated);
            var slug = command.Slug.Slugify();
            var categorySlug = _productCategoryRepository.GetCategoryWithSlug(command.CategoryId);
            var path = $"{categorySlug}/{slug}";
            var picturePath = _fileUploader.Upload(command.Picture, path);
            var product = new Product(command.Name, command.Code, command.ShortDescription, command.Description,
                picturePath, command.PictureAlt, command.PictureTitle, command.CategoryId, command.Keywords, slug,
                command.MetaDescription);
            _productRepository.Create(product);
            _productRepository.SaveChanges();
            return operation.Succedded();
        }

        public OperationResult Edit(EditProduct command)
        {
            var operation = new OperationResult();
            var product = _productRepository.GetProductWithCategory(command.Id);
            if (product == null)
                return operation.Failed(ApplicationValidationMessages.NotExisted);
            if (_productRepository.Exists(x => x.Name == command.Name&&x.Id !=command.Id))
                return operation.Failed(ApplicationValidationMessages.Duplicated);

            var slug = command.Slug.Slugify();

            var path = $"{product.ProductCategory.Slug}/{product.Slug}";
            var picturePath = _fileUploader.Upload(command.Picture, path);

            product.Edit(command.Name,command.Code,command.ShortDescription,command.Description
                ,picturePath,command.PictureAlt,command.PictureTitle,command.CategoryId,
                command.Keywords,slug,command.MetaDescription);
            _productRepository.SaveChanges();
            return operation.Succedded();
        }

        public EditProduct? GetDetails(long id)
        {
            return _productRepository.GetDetails(id);
        }
        
        public List<ProductViewModel> GetProducts()
        {
            return _productRepository.GetProducts();
        }
        
        public List<ProductViewModel> Search(ProductSearchModel searchModel)
        {
            return _productRepository.Search(searchModel);
        }
    }
}
