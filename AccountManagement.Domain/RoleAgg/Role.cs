using _0_Framework.Domain;
using AccountManagement.Domain.UserAgg;

namespace AccountManagement.Domain.RoleAgg
{
    public class Role:EntityBase
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public List<User> Users{ get; private set; }

        protected Role() => Users = new List<User>();
        
        public Role(string name,string description)
        {
            Name = name;
            Description = description;
            
        }

        public void Edit(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
