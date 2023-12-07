using Cat_a_logAPI.Data;
using Cat_a_logAPI.Service.Interfaces;

namespace Cat_a_logAPI.Service.Implementation
{
    public class DependencyService : IDependencyService
    {
        private readonly Cat_a_logBContext _dbContext;

        public DependencyService(Cat_a_logBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddDependency(Dependency dependency)
        {
            _dbContext.Dependency.Add(dependency);
            _dbContext.SaveChanges();
        }

        public void RemoveDependency(Dependency dependency)
        {
            _dbContext.Dependency.Remove(dependency);
            _dbContext.SaveChanges();

            Dependency newDependency = _dbContext.Dependency.Where(d => d.PredecessorTaskId == d.SuccessorTaskId).FirstOrDefault();
            if (newDependency != null)
            {
                _dbContext.Dependency.Remove(newDependency);
                _dbContext.SaveChanges();
            }
        }

        public void AddDependencies(List<Dependency> dependencies)
        {
            foreach (Dependency dependency in dependencies)
            {
                _dbContext.Dependency.Add(dependency);
            }
            _dbContext.SaveChanges();
        }

        public void RemoveDependencies(List<Dependency> dependencies, int taskId)
        {
            foreach (Dependency dependency in dependencies)
            {
                if (taskId == dependency.SuccessorTaskId)
                {
                    RemoveDependency(dependency);
                }
            }
            _dbContext.SaveChanges();
        }

        public List<Dependency> GetDependencies()
        {
            return _dbContext.Dependency.ToList();
        }
    }
}
