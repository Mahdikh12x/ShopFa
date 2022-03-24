using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AccountManagement.Application.Contract.User;
using AccountManagement.Domain.UserAgg;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infrastructure.EFCore.Repository
{
    public class UserRepository:BaseRepository<long,User>,IUserRepository
    {
        private readonly AccountContext _context;

        public UserRepository(AccountContext context):base(context)
        {
            _context = context;
        }

        public EditUser? GetDetails(long id)
        {
            return _context.Users.Select(user=>new EditUser
            {
                Id = user.Id,
                Fullname = user.Fullname,
                Mobile=user.Mobile,
                Username=user.Username,
                RoleId=user.RoleId,
            }).FirstOrDefault(u => u.Id == id);
        }

        public List<UserViewModel> Search(UserSearchModel searchModel)
        {
            var users = _context.Users.Include(x => x.Role).Select(user=> new UserViewModel
            {
                Fullname=user.Fullname,
                Mobile = user.Mobile,
                RoleId=user.RoleId,
                ProfilePicture=user.ProfilePicture,
                Username=user.Username,
                Id=user.Id,
                Role=user.Role.Name,
                CreationDate=user.CreationDate.ToFarsi(),
                IsActive=user.IsActive,
            }).OrderByDescending(x=>x.Id).ToList();

            if (!string.IsNullOrEmpty(searchModel.Username))
                users = users.Where(u => u.Username.Contains(searchModel.Username)).ToList();

            if (!string.IsNullOrEmpty(searchModel.Mobile))
                users = users.Where(u => u.Mobile.Contains(searchModel.Mobile)).ToList();

            if (searchModel.RoleId>0)
                users = users.Where(u => u.RoleId==searchModel.RoleId).ToList();

            return users;
        }
    }
}
