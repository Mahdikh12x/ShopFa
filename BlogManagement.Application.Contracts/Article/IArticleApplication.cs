using _0_Framework.Application;

namespace BlogManagement.Application.Contracts.Article;

public interface IArticleApplication
{
    OperationResult Create(CreateArticle command);
    OperationResult Edit(EditArticle command);
    OperationResult Active(long id);
    OperationResult DeActive(long id);
    EditArticle? GetDetails(long id);
    List<ArticleViewModel> Search(ArticleSearchModel searchModel);

}