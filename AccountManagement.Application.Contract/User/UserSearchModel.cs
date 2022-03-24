namespace AccountManagement.Application.Contract.User
{
    public class UserSearchModel
    {
        public string? Username { get; set; }
        public long RoleId { get;  set; }
        public string? Role { get; set; }
        public string? Mobile { get;  set; }
    }
}
