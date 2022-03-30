namespace _0_Framework.Application
{
    public interface IAuthHelper
    {
        void SignOut();
        bool IsAuthenticated();
        void Signin(AccountViewModel account);
    }
}
