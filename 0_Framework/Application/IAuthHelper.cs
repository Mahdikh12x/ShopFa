namespace _0_Framework.Application
{
    public interface IAuthHelper
    {
        void SignOut();
        bool IsAuthenticated();
        void Signin(AccountViewModel account);
        string? GetCurrentInfo();
        AccountViewModel? GetAccountInfo();
        List<int>? GetPermissions();
    }
}
