using _0_Framework.Application;

namespace CommentManagement.Application.Contract.Comment;

public interface ICommentApplication
{
    bool AddComment(AddComment command);
    OperationResult Confirm(long id);
    OperationResult Cancel(long id);
    List<CommentViewModel> Search(CommentSearchModel searchModel);
}