using _0_Framework.Application;
using AccountManagement.Application.Contract.Role;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace AccountManagement.Application.Contract.User
{
    public class RegisterUser
    {
        [Required(ErrorMessage = ValidationMessages.Required)]
        public string Fullname { get;  set; }
        [Required(ErrorMessage = ValidationMessages.Required)]
        public string Username { get;  set; }
        [Required(ErrorMessage = ValidationMessages.Required)]
        public string Password { get;  set; }
        [Required(ErrorMessage = ValidationMessages.Required)]
        public string Mobile { get;  set; }
        [Range(1,long.MaxValue,ErrorMessage =ValidationMessages.Required)]
        public long RoleId { get;  set; }
        public IFormFile? ProfilePicture { get;  set; }
        public List<RoleViewModel> Roles { get; set; }
    }

}
