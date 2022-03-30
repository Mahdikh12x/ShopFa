using AccountManagement.Application.Contract.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class AccountModel : PageModel
    {
        private readonly IUserApplication _userApplication;
        [TempData]
        public string Message { get; set; }
        public AccountModel(IUserApplication userApplication)
        {
            _userApplication = userApplication;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPostLogin(LoginUser command)
        {
            var result = _userApplication.Login(command);
            if (!result.IsSuccess)
            {
                Message = result.Message;
                return RedirectToPage("./Account");
            }

            return RedirectToPage("/Index");

        }
        public void OnGetSignOut()
        {
            _userApplication.SignOut();
        }
    }
}
