using CatAPI.Data;

namespace CatAPI.Service.Interfaces
{
    public interface IProjectService
    {
        public bool AddProject(Project project);

        public bool AddProjects(IEnumerable<Project> projects);

        public bool RemoveProject(int id);

        public bool RemoveProjects(IEnumerable<Project> projects);

        public Project GetProject(int Id);

        public IEnumerable<Project> GetProjects();

        public void UpdateProject(Project project);

        public bool ProjectExists(int id);
    }
}
