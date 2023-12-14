using cat_a_logB.Data;

namespace cat_a_logB.Service.Interfaces
{
    public interface IDependencyService
    {
        public bool AddDependency(Dependency dependency);

        public bool RemoveDependency(int id);

        public bool AddDependencies(List<Dependency> dependencies);

        public bool RemoveDependencies(List<Dependency> dependencies, int taskId);

        public Dependency GetDependency(int id);

        public List<Dependency> GetDependencies();

        public bool UpdateDependency(Dependency dependency);

        public bool DependencyExists(int id);
    }
}