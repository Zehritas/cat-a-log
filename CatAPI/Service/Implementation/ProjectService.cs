using Cat_a_logAPI.Data;
using Cat_a_logAPI.Service.Interfaces;

namespace Cat_a_logAPI.Service.Implementation
{
    public class ProjectService : IProjectService
    {
        private readonly Cat_a_logBContext _dbContext;

        public ProjectService(Cat_a_logBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool AddProject(Project project)
        {
            _dbContext.Project.Add(project);
            return Save();
        }

        public bool AddProjects(IEnumerable<Project> projects)
        {
            _dbContext.Project.AddRange(projects);
            return Save();
        }

        public Project GetProject(int Id)
        {
            return _dbContext.Project.Find(Id);
        }

        public IEnumerable<Project> GetProjects()
        {
            return _dbContext.Project.ToList();
        }

        public bool ProjectExists(int id)
        {
            return _dbContext.Project.Any(p => p.Id == id);
        }

        public bool RemoveProject(int id)
        {
            Project project = _dbContext.Project.Find(id);
            _dbContext.Project.Remove(project);
            return Save();
        }

        public bool RemoveProjects(IEnumerable<Project> projects)
        {
            _dbContext.Project.RemoveRange(projects);
            return Save();
        }

        public bool Save()
        {
            var saved = _dbContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public void UpdateProject(Project project)
        {
            _dbContext.Project.Update(project);
        }
    }
}
