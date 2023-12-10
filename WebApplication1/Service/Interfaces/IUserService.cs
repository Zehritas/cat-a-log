using Cat_a_logAPI.Data;

namespace Cat_a_logAPI.Service.Interfaces
{
    public interface IUserService
    {
        public bool AddUser(User user);

        public bool AddUsers(IEnumerable<User> users);

        public bool RemoveUser(User user);

        public bool RemoveUsers(IEnumerable<User> users);

        public User GetUser(int Id);

        public IEnumerable<User> GetUsers();

        public bool UpdateUser(User user);

        public bool UserExists(int id);
    }
}
