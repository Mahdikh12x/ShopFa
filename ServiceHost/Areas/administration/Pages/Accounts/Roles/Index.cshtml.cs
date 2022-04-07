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

       

    }
}
