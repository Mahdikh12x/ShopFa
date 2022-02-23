using _0_Framework.Domain;
using BlogManagement.Domain.ArticleCategoryAgg;

namespace BlogManagement.Domain.ArticleAgg;

public class Article:EntityBase
{
    public string Title { get; private set; }
    public string ShortDescription { get; private set; }
    public string Body { get; private set; }
    public string Picture { get; private set; }
    public string PictureAlt { get; private set; }
    public string PictureTitle { get; private set; }
    public DateTime PublishDate { get; private set; }
    public long CategoryId { get; set; }
    public string Slug { get; private set; }
    public string Keywords { get; private set; }
    public string MetaDescription { get; private set; }
    public string CanonicalAddress { get; private set; }
    public bool IsActive { get; private set; }
    public ArticleCategory Category { get; private set; }


    public Article(string title, string shortDescription, string body, string picture, string pictureAlt, string pictureTitle, DateTime publishDate, long categoryId, string slug, string keywords, string metaDescription, string canonicalAddress)
    {
        Title = title;
        ShortDescription = shortDescription;
        Body = body;
        Picture = picture;
        PictureAlt = pictureAlt;
        PictureTitle = pictureTitle;
        PublishDate = publishDate;
        CategoryId = categoryId;
        Slug = slug;
        Keywords = keywords;
        MetaDescription = metaDescription;
        CanonicalAddress = canonicalAddress;
        IsActive = true;
    } 

    public void Edit(string title, string shortDescription, string body, string picture, string pictureAlt, string pictureTitle, DateTime publishDate, long categoryId, string slug, string keywords, string metaDescription, string canonicalAddress)
    {
        Title = title;
        ShortDescription = shortDescription;
        Body = body;

        if(!string.IsNullOrWhiteSpace(picture))
            Picture = picture;

        PictureAlt = pictureAlt;
        PictureTitle = pictureTitle;
        PublishDate = publishDate;
        CategoryId = categoryId;
        Slug = slug;
        Keywords = keywords;
        MetaDescription = metaDescription;
        CanonicalAddress = canonicalAddress;
    }

    public void Active()
    {
        IsActive = true;
    }

    public void DeActive()
    {
        IsActive = false;
    }
}