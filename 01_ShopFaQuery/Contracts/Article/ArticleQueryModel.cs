using _01_ShopFaQuery.Contracts.Comment;

namespace _01_ShopFaQuery.Contracts.Article
{
    public class ArticleQueryModel
    {
        public long Id { get; set; }
        public string Title { get;  set; }
        public string ShortDescription { get;  set; }
        public string Body { get;  set; }
        public string Picture { get;  set; }
        public string PictureAlt { get;  set; }
        public string PictureTitle { get;  set; }
        public string PublishDate { get;  set; }
        public long CategoryId { get; set; }
        public string Slug { get;  set; }
        public List<string> KeywordList { get; set; }
        public string CategoryName{ get;  set; }
        public string CategorySlug{ get;  set; }
        public string Keywords { get;  set; } 
        public string MetaDescription { get;  set; }
        public string CanonicalAddress { get;  set; }
        public bool IsActive { get;  set; }
        public List<CommentQueryModel>? Comments { get; set; }
    }
}
