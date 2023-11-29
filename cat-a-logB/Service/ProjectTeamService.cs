using cat_a_logB.Data;
using cat_a_logB.Pages;
using Microsoft.Build.Framework;

namespace cat_a_logB.Service
{
    public class ProjectTeamService :IProjectTeamService
    {
        private readonly cat_a_logBContext _dbContext;

        public ProjectTeamService(cat_a_logBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddTeam(ProjectTeam projectTeam)
        {
            _dbContext.ProjectTeam.Add(projectTeam);
            _dbContext.SaveChanges();
        }

        public void RemoveTask(TaskData taskToRemove)
        {
            List<Dependency> dependenciesToRemove;
            List<ProjectTeam> allTeams = _dbContext.ProjectTeam.ToList();
            foreach (ProjectTeam team in allTeams)
            {
                foreach (TaskData task in team.Tasks)
                {
                    dependenciesToRemove = _dbContext.Dependency.Where(d => d.SuccessorTaskId == taskToRemove.Id).ToList();
                    _dbContext.RemoveRange(dependenciesToRemove);
                }
            }

            _dbContext.TaskData.Remove(taskToRemove);
            _dbContext.SaveChanges();
        }

        public void RemoveTeam(ProjectTeam projectTeam)
        {
            List<TaskData> teamTasks = projectTeam.Tasks;
            
            for (int i = teamTasks.Count-1; i >=0; i--)
            {
                RemoveTask(teamTasks.ElementAt(i));
            }

            _dbContext.ProjectTeam.Remove(projectTeam);
            _dbContext.SaveChanges();
        }

        public void AddTeams(List<ProjectTeam> projectTeams)
        {
            foreach (ProjectTeam projectTeam in projectTeams)
            {
                _dbContext.ProjectTeam.Add(projectTeam);
            }
            _dbContext.SaveChanges();
        }

        public void RemoveTeams(List<ProjectTeam> projectTeams)
        {
            foreach (ProjectTeam projectTeam in projectTeams)
            {
                _dbContext.ProjectTeam.Remove(projectTeam);
            }
            _dbContext.SaveChanges();
        }

        public List<ProjectTeam> GetAllTeams()
        {
            return _dbContext.ProjectTeam.ToList();
        }
    }
}
