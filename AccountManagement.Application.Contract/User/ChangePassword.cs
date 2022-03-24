using _0_Framework.Application;
using System.ComponentModel.DataAnnotations;

namespace AccountManagement.Application.Contract.User
{
    public class ChangePassword
    {
        public long Id { get; set; }
        [Required(ErrorMessage = ValidationMessages.Required)]
        public string Password { get; set; }
        [Required(ErrorMessage = ValidationMessages.Required)]
        public string RePassword { get; set; }
    }
}
