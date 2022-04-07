using _0_Framework.Infrastructure;
using AccountManagement.Application.Contract.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.administration.Pages.Accounts.Roles
{
    public class EditModel : PageModel
    {
        private readonly IRoleApplication _roleApplication;
        private readonly IEnumerable<IPermissionExposure> _permissionExposure;
        public EditRole Role = null!;
        public List<SelectListItem> Permissions=new();
        public EditModel(IRoleApplication roleApplication, IEnumerable<IPermissionExposure> permissionExposure)
        {
            _roleApplication = roleApplication;
            _permissionExposure = permissionExposure;
        }
        public void OnGet(long id)
        {
            Role=_roleApplication.GetDetails(id)!;

            foreach (var permissionExposure in _permissionExposure)
            {
                var expose = permissionExposure.Expose();
                foreach (var (key,value) in expose)
                {
                    var group = new SelectListGroup()
                    {
                        Name = key
                    };
                    foreach (var permission in value)
                    {
                        var item = new SelectListItem(permission.Name, permission.Code.ToString())
                        {
                            Group = group
                        };
                        if (Role.MappedPermissions != null && Role.MappedPermissions.Any(x => x.Code == permission.Code))
                            item.Selected = true;
                        Permissions.Add(item);
                    }
                }
            }
        }
        public IActionResult OnPost(EditRole role)
        {
            var result = _roleApplication.Edit(role);

            return RedirectToPage("Index", result);
        }
    }
}
