using _01_ShopFaQuery.Contracts.Slide;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents.SlideViewComponent
{
    public class Slide:ViewComponent
    {
        private readonly ISlideQuery _slideQuery;

        public Slide(ISlideQuery slideQuery)
        {
            _slideQuery = slideQuery;
        }

        public IViewComponentResult Invoke()
        {
            var Slide = _slideQuery.GetList();
            return View(Slide);
        }
    }
}
