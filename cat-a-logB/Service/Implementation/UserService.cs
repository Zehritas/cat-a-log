using cat_a_logB.Data;
using cat_a_logB.Service.Interfaces;

namespace cat_a_logB.Service.Implementation
{
    public class UserService : IUserService
    {
        private readonly cat_a_logBContext _dbContext;

        public UserService(cat_a_logBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool AddUser(User user) 
        {
            _dbContext.User.Add(user);
            return Save();
        }

        public bool AddUsers(IEnumerable<User> users) 
        {
            _dbContext.User.AddRange(users);
            return Save();
        }

        public bool RemoveUser(User user)
        {
            _dbContext.User.Remove(user);
            return Save();
        }

        public bool RemoveUsers(IEnumerable<User> users)
        {
            _dbContext.User.RemoveRange(users);
            return Save();
        }

        public User GetUser(int id)
        {
            User user = _dbContext.User.Find(id);
            return user;
        }

        public IEnumerable<User> GetUsers()
        {
            return _dbContext.User.ToList();
        }

        public bool Save()
        {
            var saved = _dbContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateUser(User user)
        {
            _dbContext.User.Update(user);

            return Save();
        }

        public bool UserExists(int id)
        {
            return _dbContext.User.Any(u => u.Id == id);
        }
    }
}
