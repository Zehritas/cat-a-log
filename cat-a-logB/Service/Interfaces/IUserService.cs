using cat_a_logB.Data;

namespace cat_a_logB.Service.Interfaces
{
    public interface IUserService
    {
        public bool AddUser(User user);

        public bool AddUsers(List<User> users);

        public bool RemoveUser(int id);

        public bool RemoveUsers(List<User> users);

        public User GetUser(int Id);

        public List<User> GetUsers();

        public bool UpdateUser(User user);

        public bool UserExists(int id);
    }
}
