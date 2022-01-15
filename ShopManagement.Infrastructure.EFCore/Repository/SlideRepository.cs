using System.Globalization;
using _0_Framework.Infrastructure;
using ShopManagement.Application.Contracts.Slide;
using ShopManagement.Domain.SlideAgg;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class SlideRepository:BaseRepository<long,Slide>,ISlideRepository
    {
        private readonly ShopContext _context;

        public SlideRepository( ShopContext context) : base(context)
        {
            _context = context;
        }

        public EditSlide GetDetails(long id)
        {
            var slide = _context.Slides.Select(x => new EditSlide
            {
                Id = x.Id,
                Heading = x.Heading,
                Title = x.Title,
                BtnText = x.BtnText,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Text = x.Text
            }).FirstOrDefault(x => x.Id == id);
            return slide;
        }

        public List<SlideViewModel> GetList()
        {
            return _context.Slides.Select(x => new SlideViewModel
            {
                Picture = x.Picture,
                CreationDate = x.CreationDate.ToString(CultureInfo.InvariantCulture),
                Id = x.Id,
                IsRemoved = x.IsRemoved,
                Title = x.Title
                
            }).ToList();
        }
    }
}
