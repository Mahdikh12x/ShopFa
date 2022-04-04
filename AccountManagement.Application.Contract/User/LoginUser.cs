using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;

namespace AccountManagement.Application.Contract.User
{
    public class LoginUser
    {
        [Required(ErrorMessage = ValidationMessages.Required)]
        public string Password { get; set; }
        [Required(ErrorMessage = ValidationMessages.Required)]
        public string Username{ get; set; }
    }
}
