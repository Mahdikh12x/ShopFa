using _0_Framework.Application;

namespace AccountManagement.Application.Contract.User
{
    public interface IUserApplication
    {
        OperationResult Register(RegisterUser command);
        OperationResult Edit(EditUser command);
        OperationResult  Login(LoginUser command); 
        EditUser? GetDetails(long id);
        UserViewModel? GetUserSmsInfo(long accountId);
        OperationResult ChangePassword(ChangePassword command);
        List<UserViewModel> Search(UserSearchModel searchModel);
        bool Active(long id);
        bool DeActive(long id);
        void SignOut();
    }
}
