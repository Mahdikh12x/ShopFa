namespace _0_Framework.Application
{
    public class AccountViewModel
    {
        public long Id { get; set; }
        public long RoleId { get; set; }
        public string Role { get; set; }
        public string Username { get; set; }
        public string Fullname { get; set; }
        public string Password { get; set; }
        public string Mobile { get; set; }

        public AccountViewModel(long id, long roleId, string role, string username,string fullname, string password, string mobile)
        {
            Id = id;
            RoleId = roleId;
            Role = role;
            Username = username;
            Fullname= fullname;
            Password = password;
            Mobile = mobile;
        }
    }
}
