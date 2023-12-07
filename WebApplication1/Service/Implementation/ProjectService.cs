using Cat_a_logAPI.Data;
using Cat_a_logAPI.Service.Interfaces;

namespace Cat_a_logAPI.Service.Implementation
{
    public class ProjectService : IProjectService
    {
        private readonly Cat_a_logBContext _dbContext;

        public ProjectService(Cat_a_logBContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
