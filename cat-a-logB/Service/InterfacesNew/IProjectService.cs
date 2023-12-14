using cat_a_logB.Data;

namespace cat_a_logB.Service.InterfacesNew
{
    public interface IProjectService
    {
        public bool AddProject(Project project);

        public bool AddProjects(IEnumerable<Project> projects);

        public bool RemoveProject(int id);

        public bool RemoveProjects(IEnumerable<Project> projects);

        public Project GetProject(int Id);

        public IEnumerable<Project> GetProjects();

        public bool UpdateProject(Project project);

        public bool ProjectExists(int id);
    }
}
