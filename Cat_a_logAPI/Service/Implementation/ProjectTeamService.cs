using Cat_a_logAPI.Data;
using Cat_a_logAPI.Service.Interfaces;

namespace Cat_a_logAPI.Service.Implementation
{
    public class ProjectTeamService : IProjectTeamService
    {
        private readonly Cat_a_logBContext _dbContext;
        private readonly ITaskDataService _taskDataService;

        public ProjectTeamService(Cat_a_logBContext dbContext, ITaskDataService taskDataService)
        {
            _dbContext = dbContext;
            _taskDataService = taskDataService;
        }

        public bool AddTeam(ProjectTeam projectTeam)
        {
            _dbContext.ProjectTeam.Add(projectTeam);
            return Save();
        }

        public bool RemoveTeam(ProjectTeam projectTeam)
        {
            List<TaskData> teamTasks = projectTeam.Tasks;

            for (int i = teamTasks.Count - 1; i >= 0; i--)
            {
                _taskDataService.RemoveTask(teamTasks[i]);
                //RemoveTask(teamTasks.ElementAt(i));
            }

            _dbContext.ProjectTeam.Remove(projectTeam);
            return Save();
        }

        public bool AddTeams(IEnumerable<ProjectTeam> projectTeams)
        {
            foreach (ProjectTeam projectTeam in projectTeams)
            {
                _dbContext.ProjectTeam.Add(projectTeam);
            }
            return Save();
        }

        public bool RemoveTeams(IEnumerable<ProjectTeam> projectTeams)
        {
            foreach (ProjectTeam projectTeam in projectTeams)
            {
                RemoveTeam(projectTeam);
            }
            return Save();
        }

        public ProjectTeam GetTeam(int Id)
        {
            return _dbContext.ProjectTeam.Find(Id);
        }

        public IEnumerable<ProjectTeam> GetTeams()
        {
            return _dbContext.ProjectTeam.ToList();
        }

        public bool Save()
        {
            var saved = _dbContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateTeam(ProjectTeam team)
        {
            _dbContext.ProjectTeam.Update(team);

            return Save();
        }

        public bool TeamExists(int id)
        {
            return _dbContext.ProjectTeam.Any(t => t.Id == id);
        }
    }
}
