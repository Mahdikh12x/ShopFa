namespace ShopManagement.Domain.Services
{
    public interface IShopAccountAcl
    {
        (string number, string name) GetUserInformation(long accountId);
    }
}
