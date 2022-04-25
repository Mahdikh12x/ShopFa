using AccountManagement.Application.Contract.User;
using ShopManagement.Domain.Services;

namespace ShopManagement.Infrastructure.Account.ACL
{
    public class ShopAccountAcl:IShopAccountAcl
    {
        private readonly IUserApplication _userApplication;

        public ShopAccountAcl(IUserApplication userApplication)
        {
            _userApplication = userApplication;
        }

        public (string number, string name) GetUserInformation(long accountId)
        {
            var currentUser = _userApplication.GetUserSmsInfo(accountId);
            return currentUser!= null ? (currentUser.Mobile, currentUser.Fullname) : ("","");
        }
    }
}