using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents.HeaderViewComponent
{
    public class HeaderViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
