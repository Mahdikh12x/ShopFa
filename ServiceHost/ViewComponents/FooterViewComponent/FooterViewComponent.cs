using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents.FooterViewComponent
{
    public class FooterViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
