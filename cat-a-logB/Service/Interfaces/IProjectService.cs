using cat_a_logB.Data;

namespace cat_a_logB.Service.Interfaces
{
    public interface IProjectService
    {
        public bool AddProject(Project project);

        public bool AddProjects(IEnumerable<Project> projects);

        public bool RemoveProject(Project project);

        public bool RemoveProjects(IEnumerable<Project> projects);

        public Project GetProject(int Id);

        public IEnumerable<Project> GetProjects();

        public void UpdateProject(Project project);

        public bool ProjectExists(int id);
    }
}
