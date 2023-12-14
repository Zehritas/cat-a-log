using cat_a_logB.Data;

namespace cat_a_logB.Service.InterfacesNew
{
    public interface IDependencyService
    {
        public bool AddDependency(Dependency dependency);

        public bool RemoveDependency(int id);

        public bool AddDependencies(IEnumerable<Dependency> dependencies);

        public bool RemoveDependencies(IEnumerable<Dependency> dependencies, int taskId);

        public Dependency GetDependency(int id);

        public IEnumerable<Dependency> GetDependencies();

        public bool UpdateDependency(Dependency dependency);

        public bool DependencyExists(int id);
    }
}