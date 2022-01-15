using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.Slide;

namespace ServiceHost.Areas.administration.Pages.Shop.Slides
{
    public class IndexModel : PageModel
    {
        private readonly ISlideApplication _slideApplication;
        public List<SlideViewModel> Slides;
        public IndexModel(ISlideApplication slideApplication)
        {
            _slideApplication = slideApplication;
        }

        public void OnGet()
        {
            Slides = _slideApplication.GetList();
        }

        public PartialViewResult OnGetCreate()
        {
            var command = new CreateSlide();
            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(CreateSlide command)
        {
            var result = _slideApplication.Create(command);
            return new JsonResult(result);
        }
        public PartialViewResult OnGetEdit(long id)
        {
            var command = _slideApplication.GetDetails(id);
            return Partial("./Edit", command);
        }

        public JsonResult OnPostEdit(EditSlide command)
        {
            var result = _slideApplication.Edit(command);
            return new JsonResult(result);
        }

        public RedirectToPageResult OnGetRemove(long id)
        {
            var result = _slideApplication.Remove(id);
            return RedirectToPage(result);
        }
        public RedirectToPageResult OnGetRestore(long id)
        {
            var result = _slideApplication.Restore(id);
            return RedirectToPage(result);
        }
    }
}
