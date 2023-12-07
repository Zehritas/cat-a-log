using Cat_a_logAPI.Data;
using Cat_a_logAPI.Service.Interfaces;

namespace Cat_a_logAPI.Service.Implementation
{
    public class MemberService : IMemberService
    {
        private readonly Cat_a_logBContext _dbContext;

        public MemberService(Cat_a_logBContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}