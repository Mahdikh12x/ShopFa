using _0_Framework.Application;
using BlogManagement.Application.Contracts.Article;
using BlogManagement.Application.Contracts.ArticleCategory;
using BlogManagement.Domain.ArticleAgg;
using BlogManagement.Domain.ArticleCategoryAgg;

namespace BlogManagement.Application
{
    public class ArticleApplication:IArticleApplication
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IFileUploader _fileUploader;
        private readonly IArticleCategoryRepository _articleCategoryRepository;
        public ArticleApplication(IArticleRepository articleRepository, IFileUploader fileUploader, IArticleCategoryRepository articleCategoryRepository)
        {
            _articleRepository = articleRepository;
            _fileUploader = fileUploader;
            _articleCategoryRepository = articleCategoryRepository;
        }

        public OperationResult Create(CreateArticle command)
        {
            var operation = new OperationResult();
            
            if (_articleRepository.Exists(x => x.Title == command.Title))
                return operation.Failed(ApplicationValidationMessages.Duplicated);

            var categorySlug = _articleCategoryRepository.GetArticleCategorySlug(command.CategoryId);

            var publishDate = command.PublishDate.ToGeorgianDateTime();
            var slug = command.Slug.Slugify();
            var path = $"blog/{categorySlug}/{slug}";
            var pictureName = _fileUploader.Upload(command.Picture, path);

            var article = new Article(command.Title, command.ShortDescription, command.Body, pictureName, command.PictureAlt,
                command.PictureTitle, publishDate, command.CategoryId,slug, command.Keywords,
                command.MetaDescription, command.CanonicalAddress);
            _articleRepository.Create(article);
            _articleRepository.SaveChanges();
            return operation.Succedded();
        }

        public OperationResult Edit(EditArticle command)
        {
            var operation=new OperationResult();
            var article = _articleRepository.GetArticleWithCategory(command.Id);

            if (article == null)
                return operation.Failed(ApplicationValidationMessages.NotExisted);

            if (_articleRepository.Exists(x => x.Title == command.Title && x.Id != command.Id))
                return operation.Failed(ApplicationValidationMessages.Duplicated);
            var publishDate = command.PublishDate.ToGeorgianDateTime();
            var slug = command.Slug.Slugify();
            var path = $"blog/{article.Category.Slug}/{article.Slug}";
            var pictureName = _fileUploader.Upload(command.Picture, path);

            article.Edit(command.Title,command.ShortDescription,command.Body,pictureName
                ,command.PictureAlt,command.PictureTitle,publishDate,command.CategoryId,slug,command.Keywords,command.MetaDescription,command.CanonicalAddress);
            _articleRepository.SaveChanges();
            return operation.Succedded();
        }

        public OperationResult Active(long id)
        {
            var operation = new OperationResult();
            var article = _articleRepository.Get(id);
            if (article == null)
                return operation.Failed(ApplicationValidationMessages.NotExisted);

            article.Active();
            _articleRepository.SaveChanges();
            return operation.Succedded();
        }

        public OperationResult DeActive(long id)
        {
            var operation = new OperationResult();
            var article = _articleRepository.Get(id);
            if (article == null)
                return operation.Failed(ApplicationValidationMessages.NotExisted);

            article.DeActive();
            _articleRepository.SaveChanges();
            return operation.Succedded();
        }

        public EditArticle? GetDetails(long id)
        {
            return _articleRepository.GetDetails(id);
        }

        public List<ArticleViewModel> Search(ArticleSearchModel searchModel)
        {
            return _articleRepository.Search(searchModel);
        }
    }
}
