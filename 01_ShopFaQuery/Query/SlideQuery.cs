using _01_ShopFaQuery.Contracts.Slide;
using ShopManagement.Infrastructure.EFCore;

namespace _01_ShopFaQuery.Query
{
    public class SlideQuery : ISlideQuery
    {
        private readonly ShopContext _shopContext;

        public SlideQuery(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public List<SlideQueryModel> GetList()
        {
            return _shopContext.Slides.Where(x=>x.IsRemoved==false).Select(x => new SlideQueryModel
            {
                Heading = x.Heading,
                Title = x.Title,
                BtnText = x.BtnText,
                Link = x.Link,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Text = x.Text
            }).ToList();
        }
    }
}
