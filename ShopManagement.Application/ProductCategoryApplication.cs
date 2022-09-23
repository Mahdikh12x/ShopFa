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

            var path = $"{command.Slug.Slugify()}";
            var picturePath = _fileUploader.Upload(command.Picture, path);

            var productCategory = new ProductCategory(command.Name,command.Description,command.MetaDescription,
                picturePath,command.PictureTitle
                ,command.PictureAlt,slug,command.Keywords);
            _productCategoryRepository.Create(productCategory);
            _productCategoryRepository.SaveChanges();
            return operation.Succedded();
        }

        public OperationResult Edit(EditProductCategory command)
        {
            var operation = new OperationResult();
            var productCategory = _productCategoryRepository.Get(command.Id);

            if (productCategory is null)
                return operation.Failed(ApplicationValidationMessages.NotExisted);
            if (_productCategoryRepository.Exists(x => x.Name == command.Name&&x.Id !=command.Id))
                return operation.Failed(ApplicationValidationMessages.Duplicated);

            var path = $"{command.Slug.Slugify()}";
            var picturePath = _fileUploader.Upload(command.Picture, path);

            var slug = command.Slug.Slugify();
            productCategory.Edit(command.Name, command.Description, command.MetaDescription,
                picturePath, command.PictureTitle, command.PictureAlt, slug, command.Keywords);

            _productCategoryRepository.SaveChanges();
            return operation.Succedded();

        }

        public EditProductCategory GetDetails(long id)=>
            _productCategoryRepository.GetDetails(id);
        

        public async Task<List<ProductCategoryViewModel>?> GetProductCategoriesAsync()
            =>await _productCategoryRepository.GetProductCategoriesAsync();
        

        public async Task<List<ProductCategoryViewModel>?> SearchAsync(ProductCategorySearchModel searchModel)
            =>await _productCategoryRepository.SearchAsync(searchModel);
        
    }
}
