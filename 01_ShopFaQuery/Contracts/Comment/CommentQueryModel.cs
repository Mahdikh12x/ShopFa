namespace _01_ShopFaQuery.Contracts.Comment;

public class CommentQueryModel
{
    public long Id { get; set; }
    public string? Name { get;  set; }
    public string? Description { get;  set; }
    public int Type { get;  set; }
    public string? CreationDate{ get;  set; }
    public long ParentId{ get; set; }
    public bool HasParent{ get; set; }
    public string? Parent { get; set; }
    public DateTime Time { get; set; }


}