using _0_Framework.Domain;

namespace ShopManagement.Domain.ProductCategoryAgg
{
    public class ProductCategory : EntityBase
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string MetaDescription { get; private set; }
        public string Picture { get; private set; }
        public string PictureTitle { get; private set; }
        public string PictureAlt { get; private set; }

        public string Slug { get; private set; }
        public string Keywords { get; private set; }

        protected ProductCategory()
        {

        }
        public ProductCategory(string name, string description, string metaDescription
            , string picture, string pictureTitle, string pictureAlt, string slug, string keywords)
        {
            Name = name;
            Description = description;
            MetaDescription = metaDescription;
            Picture = picture;
            PictureTitle = pictureTitle;
            PictureAlt = pictureAlt;
            Slug = slug;
            Keywords = keywords;
        }
        public void Edit(string name, string description, string metaDescription
            , string picture, string pictureTitle, string pictureAlt, string slug, string keywords)
        {
            Name = name;
            Description = description;
            MetaDescription = metaDescription;
            Picture = picture;
            PictureTitle = pictureTitle;
            PictureAlt = pictureAlt;
            Slug = slug;
            Keywords = keywords;
        }
    }
}
