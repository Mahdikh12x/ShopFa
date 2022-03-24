using AccountManagement.Application.Contract.Role;
using AccountManagement.Application.Contract.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.administration.Pages.Accounts.Users
{
    public class IndexModel : PageModel
    {
        public List<UserViewModel> Users;
        public SelectList Roles;
        public UserSearchModel SearchModel;
        private readonly IUserApplication _userApplication;
        private readonly IRoleApplication _roleApplication;

        public IndexModel(IUserApplication userApplication,IRoleApplication roleApplication)
        {
            _roleApplication = roleApplication;
            _userApplication = userApplication;
        }

        public void OnGet(UserSearchModel searchModel)
        {
            Users = _userApplication.Search(searchModel);
            Roles=new SelectList(_roleApplication.GetRoles(),"Id","Name");
        }

        public IActionResult OnGetRegister()
        {
            var command = new RegisterUser
            {
                Roles = _roleApplication.GetRoles()
            };
            return Partial("./Register",command);
        }
        public JsonResult OnPostRegister(RegisterUser command)
        {
            var result = _userApplication.Register(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var user = _userApplication.GetDetails(id);
            user.Roles = _roleApplication.GetRoles();
            return Partial("./Edit", user);
        }
        public IActionResult OnPostEdit(EditUser command)
        {
            
            var result = _userApplication.Edit(command);
            return new JsonResult(result);
        } 
        public IActionResult OnGetChangePassword(long id)
        {
            var result = new ChangePassword
            {
                Id = id,
            };
            return Partial("./ChangePassword",result);
        }
        public IActionResult OnPostChangePassword(ChangePassword command)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage("./index");
            }

            var result = _userApplication.ChangePassword(command);
            return new JsonResult(result);
        }
        public RedirectToPageResult OnGetActive(long id)
        {
            _userApplication.Active(id);
               return RedirectToPage();
        }
        public RedirectToPageResult OnGetDeActive(long id)
        {
            _userApplication.DeActive(id);
                return RedirectToPage();
        }

    }
}
