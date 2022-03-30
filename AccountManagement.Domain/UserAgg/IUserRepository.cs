using _0_Framework.Domain;
using AccountManagement.Application.Contract.User;

namespace AccountManagement.Domain.UserAgg
{
    public interface IUserRepository:IRepository<long,User>
    {
        EditUser? GetDetails(long id);
        List<UserViewModel> Search(UserSearchModel searchModel);
        User? GetBy(string username);
    }
}
