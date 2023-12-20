using cat_a_logB.Data;

namespace cat_a_logB.Service.Interfaces
{
    public interface IProjectService
    {
        public bool AddProject(Project project);

        public bool AddProjects(List<Project> projects);

        public bool RemoveProject(int id);

        public bool RemoveProjects(List<Project> projects);

        public Project GetProject(int Id);

        public List<Project> GetProjects();

        public bool UpdateProject(Project project);

        public bool ProjectExists(int id);
    }
}
