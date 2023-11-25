using cat_a_logB.Data;

namespace cat_a_logB.Service
{
    public class ProjectTeamService :IProjetTeamService
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

        public void RemoveTeam(ProjectTeam projectTeam)
        {
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
    }
}
