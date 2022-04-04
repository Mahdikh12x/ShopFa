using _0_Framework.Application;
using AccountManagement.Application.Contract.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class AccountModel : PageModel
    {
        private readonly IUserApplication _userApplication;
        [TempData]
        public string LoginMessage { get; set; }
        [TempData]
        public string RegisterMessage { get; set; }

        //public LoginUser LoginUser;
        //public RegisterUser RegisterUser;
        public AccountModel(IUserApplication userApplication)
        {
            _userApplication = userApplication;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPostLogin(LoginUser command)
        {
            if (!ModelState.IsValid)
            {
                LoginMessage = ValidationMessages.Required;
                return RedirectToPage("/Account");
            }
            var result = _userApplication.Login(command);
            if (result.IsSuccess)
                return RedirectToPage("./Index");
            LoginMessage = result.Message;
            return RedirectToPage("/Account");

        }

        public IActionResult OnPostRegister(RegisterUser user,string rePassword)
        {
            if (user.Password != rePassword)
            {
                RegisterMessage = ApplicationValidationMessages.PasswordNotMatch;
                return RedirectToPage("/Account");
            }
            var result = _userApplication.Register(user);
            if (result.IsSuccess)
                return RedirectToPage("/Account");

            RegisterMessage = result.Message;
            return RedirectToPage("/Account");
        }

        public void OnGetSignOut()
        {
            _userApplication.SignOut();
        }
    }
}
