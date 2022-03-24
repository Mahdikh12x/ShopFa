namespace AccountManagement.Application.Contract.User
{
    public class UserViewModel
    {
        public long Id { get; set; }
        public string Fullname { get; set; }
        public string Username { get; set; }
        public string Mobile { get; set; }
        public long RoleId { get; set; }
        public string Role{ get; set; }
        public string ProfilePicture { get; set; }
        public string CreationDate { get; set; }
        public bool IsActive { get; set; }

    }
}
