using Cat_a_logAPI.Data;
using Cat_a_logAPI.Service.Interfaces;

namespace Cat_a_logAPI.Service.Implementation
{
    public class UserService : IUserService
    {
        private readonly Cat_a_logBContext _dbContext;

        public UserService(Cat_a_logBContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
