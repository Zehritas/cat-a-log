using cat_a_logB.Data;
using Microsoft.EntityFrameworkCore;

namespace cat_a_logB.Service
{
    public class DependencyService : IDependencySvervice
    {
        private readonly cat_a_logBContext _dbContext;

        public DependencyService(cat_a_logBContext dbContext)
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
        }

        public void AddDependencies(List<Dependency> dependencies)
        {
            foreach (Dependency dependency in dependencies)
            {
                _dbContext.Dependency.Add(dependency);
            }
            _dbContext.SaveChanges();
        }

        public void RemoveDependencies(List<Dependency> dependencies)
        {
            foreach(Dependency dependency in dependencies)
            {
                _dbContext.Dependency.Remove(dependency);
            }
            _dbContext.SaveChanges();
        }
    }
}
