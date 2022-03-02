using _0_Framework.Application;
using CommentManagement.Application.Contract.Comment;
using CommentManagement.Domain.CommentAgg;

namespace CommentManagement.Application;

public class CommentApplication : ICommentApplication
{
    private readonly ICommentRepository _commentRepository;

    public CommentApplication(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public bool AddComment(AddComment command)
    {
        var comment = new Comment(command.Name, command.Description, command.Email, command.OwnerRecordId, command.Type,
            command.ParentId);
        try
        {
            _commentRepository.Create(comment);
            _commentRepository.SaveChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public OperationResult Confirm(long id)
    {
        var operation = new OperationResult();
        var comment = _commentRepository.Get(id);
        if (comment == null)
            return operation.Failed(ApplicationValidationMessages.NotExisted);

        comment.Confirm();
        _commentRepository.SaveChanges();
        return operation.Succedded();

    }

    public OperationResult Cancel(long id)
    {
        var operation = new OperationResult();
        var comment = _commentRepository.Get(id);
        if (comment == null)
            return operation.Failed(ApplicationValidationMessages.NotExisted);

        comment.Cancel();
        _commentRepository.SaveChanges();
        return operation.Succedded();
    }

    public List<CommentViewModel> Search(CommentSearchModel searchModel)
    {
        return _commentRepository.Search(searchModel);
    }
}