using _0_Framework.Domain;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Domain.ProductAgg
{
    public class Product : EntityBase
    {
        public string Name { get; private set; }
        public string Code { get; private set; }
        public string ShortDescripion { get; private set; }
        public string Description { get; private set; }
        public double UnitePrice { get; private set; }
        public bool IsInStock { get; private set; }
        public string Picture { get; private set; }
        public string PictureAlt { get; private set; }
        public string PictureTitle { get; private set; }
        public long CategoryId { get; private set; }
        public string Keywords { get; private set; }
        public string Slug { get; private set; }
        public string MetaDescription { get; private set; }
        public ProductCategory ProductCategory { get; private set; }


        public Product(string name, string code,
            string shortDescripion, string description, double unitePrice, string picture,
            string pictureAlt, string pictureTitle, long categoryId, string keywords, string slug, string metaDescription)
        {
            Name = name;
            Code = code;
            ShortDescripion = shortDescripion;
            Description = description;
            UnitePrice = unitePrice;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            CategoryId = categoryId;
            Keywords = keywords;
            Slug = slug;
            MetaDescription = metaDescription;
            IsInStock = true;
        }
        public void Edit(string name, string code,
            string shortDescripion, string description, double unitePrice, string picture,
            string pictureAlt, string pictureTitle, long categoryId, string keywords, string slug, string metaDescription)
        {
            Name = name;
            Code = code;
            ShortDescripion = shortDescripion;
            Description = description;
            UnitePrice = unitePrice;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            CategoryId = categoryId;
            Keywords = keywords;
            Slug = slug;
            MetaDescription = metaDescription;
        }

        public void NotInStock()
        {
            IsInStock = false;
        }
        public void InStock()
        {
            IsInStock = true;
        }
    }
}
