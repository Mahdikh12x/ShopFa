namespace _0_Framework.Application
{
    public class AccountViewModel
    {
        public long Id { get; set; }
        public long RoleId { get; set; }
        public string? Role { get; set; }
        public string Username { get; set; }
        public string Fullname { get; set; }
        public string Password { get; set; }
        public string Mobile { get; set; }
        public List<int> Permissions { get; set; }

        public AccountViewModel()
        {
            
        }
        public AccountViewModel(long id, long roleId, string username,string fullname, string password, string mobile,List<int> permissions)
        {
            Id = id;
            RoleId = roleId;
            //Role = role;
            Username = username;
            Fullname= fullname;
            Password = password;
            Mobile = mobile;
            Permissions = permissions;
        }
    }
}
