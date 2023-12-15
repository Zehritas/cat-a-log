using Cat_a_logAPI.Data;

namespace Cat_a_logAPI.Service.Interfaces
{
    public interface IDependencyService
    {
        public bool AddDependency(Dependency dependency);

        public void RemoveDependency(int id);

        public bool AddDependencies(IEnumerable<Dependency> dependencies);

        public bool RemoveDependencies(IEnumerable<Dependency> dependencies, int taskId);

        public Dependency GetDependency(int id);

        public IEnumerable<Dependency> GetDependencies();

        public bool UpdateDependency(Dependency dependency);

        public bool DependencyExists(int id);
    }
}