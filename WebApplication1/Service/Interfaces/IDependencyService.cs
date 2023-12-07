using Cat_a_logAPI.Data;

namespace Cat_a_logAPI.Service.Interfaces
{
    public interface IDependencyService
    {
        public void AddDependency(Dependency dependency);

        public void RemoveDependency(Dependency dependency);

        public void AddDependencies(List<Dependency> dependencies);

        public void RemoveDependencies(List<Dependency> dependencies, int taskId);

        //public Dependency GetDependency(int id);

        public List<Dependency> GetDependencies();
    }
}