using _0_Framework.Domain;
using AccountManagement.Domain.RoleAgg;

namespace AccountManagement.Domain.UserAgg
{
    public class User: EntityBase
    {
      
        public string Fullname { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public string Mobile { get; private set; }
        public long RoleId { get; private set; }
        public string ProfilePicture { get; private set; }
        public bool IsActive { get; private set; }
        public Role Role{ get; private set; }

        public User(string fullname, string username, string password, string mobile, long roleId, string profilePicture)
        {
            Fullname = fullname;
            Username = username;
            Password = password;
            Mobile = mobile;
            RoleId = roleId;
            ProfilePicture = profilePicture;
            IsActive = true;
        }

        public void Edit(string fullname, string username, string mobile, long roleId, string profilePicture)
        {
            Fullname = fullname;
            Username = username;
            Mobile = mobile;
            RoleId = roleId;

            if(!string.IsNullOrWhiteSpace(profilePicture))
            ProfilePicture = profilePicture;
        }

        public void ChangePassword(string password)
        {
            Password= password;
        }
        public void Active()
        {
            IsActive = true;
        }

        public void DeActive()
        {
            IsActive = false;
        }

    }
}
