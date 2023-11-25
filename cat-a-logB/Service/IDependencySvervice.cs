using cat_a_logB.Data;

namespace cat_a_logB.Service
{
    public interface IDependencySvervice
    {
        public void AddDependency(Dependency dependency);

        public void RemoveDependency(Dependency dependency);

        public void AddDependencies(List<Dependency> dependencies);

        public void RemoveDependencies(List<Dependency> dependencies);


    }
}
