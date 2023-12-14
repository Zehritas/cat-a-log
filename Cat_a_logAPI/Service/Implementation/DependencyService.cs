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

        public bool AddDependency(Dependency dependency)
        {
            _dbContext.Dependency.Add(dependency);
            return Save();
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

        public bool AddDependencies(IEnumerable<Dependency> dependencies)
        {
            foreach (Dependency dependency in dependencies)
            {
                _dbContext.Dependency.Add(dependency);
            }
            return Save();
        }

        public bool RemoveDependencies(IEnumerable<Dependency> dependencies, int taskId)
        {
            foreach (Dependency dependency in dependencies)
            {
                if (taskId == dependency.SuccessorTaskId)
                {
                    RemoveDependency(dependency);
                }
            }
            return Save();
        }

        public Dependency GetDependency(int Id) 
        {
            return _dbContext.Dependency.Find(Id);
        }

        public IEnumerable<Dependency> GetDependencies()
        {
            return _dbContext.Dependency.ToList();
        }

        public bool Save()
        {
            var saved = _dbContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateDependency(Dependency dependency)
        {
            _dbContext.Dependency.Update(dependency);

            return Save();
        }

        public bool DependencyExists(int id)
        {
            return _dbContext.Dependency.Any(d => d.Id == id);
        }
    }
}
