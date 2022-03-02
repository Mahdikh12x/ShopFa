namespace CommentManagement.Application.Contract.Comment;

public class CommentViewModel
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Email { get; set; }
    public long OwnerRecordId { get; set; }
    public string Owner { get; set; }
    public int Type { get; set; }
    public long ParentId { get; set; }
    public string Parent { get; set; }
    public bool IsCanceled{ get; set; }
    public bool IsConfirmed{ get; set; }
    public string CreationDate { get; set; }
}