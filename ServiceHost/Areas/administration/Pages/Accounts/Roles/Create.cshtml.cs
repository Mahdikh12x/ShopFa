using AccountManagement.Application.Contract.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.administration.Pages.Accounts.Roles
{
    public class CreateModel : PageModel
    {
        public CreateRole Command;
        private readonly IRoleApplication _roleApplication;
        public CreateModel(IRoleApplication roleApplication)
        {
            _roleApplication = roleApplication;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost(CreateRole command)
        {
            var result = _roleApplication.Create(command);

            return RedirectToPage("Index");
        }
    }
}
