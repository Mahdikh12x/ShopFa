namespace CommentManagement.Application.Contract.Comment;

public class CommentSearchModel
{
    public int Type { get; set; }
    public string? TypeName { get; set; }
    public string? Name { get; set; }
    public bool Pending { get; set; }

}