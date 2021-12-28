﻿using _0_Framework.Application;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Application
{
    public class ProductCategoryApplication : IProductCategoryApplication
    {
        private readonly IProductCategoryRepository _productCategoryRepository;

        public ProductCategoryApplication(IProductCategoryRepository productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
        }

        public OperationResult Create(CreateProductCategory command)
        {
            var operation=new OperationResult();
            if (_productCategoryRepository.Exists(x => x.Name == command.Name))
                operation.Failed("این رکورد از قبل موجود میباشد لطفا دوباره اقدام نمایید.");
            var slug=command.Slug.Slugify();
            var productCategory=new ProductCategory(command.Name,command.Description,command.MetaDescription,command.Picture,
                command.PictureTitle,command.PictureAlt,slug,command.Keywords);

            _productCategoryRepository.Create(productCategory);
            _productCategoryRepository.SaveChanges();
            return operation.Succedded();
        }

        public OperationResult Edit(EditProductCategory command)
        {
            var operation = new OperationResult();
            var productCategory = _productCategoryRepository.Get(command.Id);
            if (productCategory != null)
                operation.Failed("این مقدار وجود ندارد لطفا دوباره اقدام نمایید .");
            if (_productCategoryRepository.Exists(x => x.Name == command.Name))
                operation.Failed("این نام تکراری میباشد لطفا ئوباره اقدام نمایید .");

            var slug = command.Slug.Slugify();
            productCategory.Edit(command.Name, command.Description, command.MetaDescription,
                command.Picture, command.PictureTitle, command.PictureAlt, slug, command.Keywords);

            _productCategoryRepository.SaveChanges();
            return operation.Succedded();

        }

        public EditProductCategory GetDetails(long id)
        {
           return _productCategoryRepository.GetDetails(id);
        }


        public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel)
        {
           return _productCategoryRepository.Search(searchModel);

        }
    }
}
