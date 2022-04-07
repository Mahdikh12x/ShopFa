using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AccountManagement.Application.Contract.Role;
using AccountManagement.Domain.RoleAgg;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infrastructure.EFCore.Repository
{
    public class RoleRepository : BaseRepository<long, Role>, IRoleRepository
    {
        private readonly AccountContext _context;
        public RoleRepository(AccountContext context):base(context)
        {
            _context = context;
        }

        public EditRole? GetDetails(long id)
        {
            var role= _context.Roles.Select(role => new EditRole
            {
            Id = role.Id,
            Name = role.Name,
            Description=role.Description,
            MappedPermissions = MappedPermissions(role.Permissions)
            }).AsNoTracking().FirstOrDefault(x => x.Id == id);

            if (role?.MappedPermissions != null) role.Permissions = role.MappedPermissions.Select(x => x.Code).ToList();
            return role;

        }

        private static List<PermissionDto> MappedPermissions(IEnumerable<Permission> rolePermissions)
        {
          return rolePermissions.Select(x => new PermissionDto(x.Code, x.Name)).ToList();

        }

        public List<RoleViewModel> GetRoles()
        {
           return _context.Roles.Select(role => new RoleViewModel
           {
               Id=role.Id,
               Name = role.Name,
               Description=role.Description,
               CreationDate=role.CreationDate.ToFarsi()
           }).ToList();
        
        }
    }
}
