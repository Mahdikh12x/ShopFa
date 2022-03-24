using AccountManagement.Application.Contract.Role;
using BlogManagement.Application.Contracts.ArticleCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.administration.Pages.Accounts.Roles
{
    public class IndexModel : PageModel
    {
        public List<RoleViewModel> Roles;
        private readonly IRoleApplication _roleApplication;
        public IndexModel(IRoleApplication roleApplication)
        {
            _roleApplication = roleApplication;
        }

        public void OnGet()
        {
            Roles = _roleApplication.GetRoles();
        }

        public IActionResult OnGetCreate()
        {
            return Partial("./Create");
        }
        public JsonResult OnPostCreate(CreateRole command)
        {
            var result = _roleApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var role = _roleApplication.GetDetails(id);
            return Partial("./Edit", role);
        }
        public IActionResult OnPostEdit(EditRole command)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage("./index");
            }

            var result = _roleApplication.Edit(command);

            return new JsonResult(result);
        }

    }
}
