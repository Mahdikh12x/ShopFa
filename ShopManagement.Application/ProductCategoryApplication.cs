using _0_Framework.Application;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Application
{
    public class ProductCategoryApplication : IProductCategoryApplication
    {
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IFileUploader _fileUploader;
        public ProductCategoryApplication(IProductCategoryRepository productCategoryRepository, IFileUploader fileUploader)
        {
            _productCategoryRepository = productCategoryRepository;
            _fileUploader = fileUploader;
        }

        public OperationResult Create(CreateProductCategory command)
        {
            var operation = new OperationResult();
            if (_productCategoryRepository.Exists(x => x.Name == command.Name))
             return   operation.Failed(ApplicationValidationMessages.Duplicated);
            var slug = command.Slug.Slugify();

            var productCategory = new ProductCategory(command.Name, command.Description, command.MetaDescription, "",
                command.PictureTitle, command.PictureAlt, slug, command.Keywords);

            _productCategoryRepository.Create(productCategory);
            _productCategoryRepository.SaveChanges();
            return operation.Succedded();
        }

        public OperationResult Edit(EditProductCategory command)
        {
            var operation = new OperationResult();
            var productCategory = _productCategoryRepository.Get(command.Id);

            if (productCategory == null)
                return operation.Failed(ApplicationValidationMessages.NotExisted);
            if (_productCategoryRepository.Exists(x => x.Name == command.Name&&x.Id !=command.Id))
                return operation.Failed(ApplicationValidationMessages.Duplicated);

            var slug = command.Slug.Slugify();
            var fileName = _fileUploader.Upload(command.Picture, slug);
            productCategory.Edit(command.Name, command.Description, command.MetaDescription,
                fileName, command.PictureTitle, command.PictureAlt, slug, command.Keywords);

            _productCategoryRepository.SaveChanges();
            return operation.Succedded();

        }

        public EditProductCategory GetDetails(long id)
        {
            return _productCategoryRepository.GetDetails(id);
        }

        public List<ProductCategoryViewModel> GetProductCategories()
        {
            return _productCategoryRepository.GetProductCategories();
        }

        public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel)
        {
            return _productCategoryRepository.Search(searchModel);

        }
    }
}
