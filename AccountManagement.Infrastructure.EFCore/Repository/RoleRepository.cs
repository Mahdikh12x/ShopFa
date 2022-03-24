using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AccountManagement.Application.Contract.Role;
using AccountManagement.Domain.RoleAgg;

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
            return _context.Roles.Select(role => new EditRole
            {
            Id = role.Id,
            Name = role.Name,
            Description=role.Description
            }).FirstOrDefault(x => x.Id == id);
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
