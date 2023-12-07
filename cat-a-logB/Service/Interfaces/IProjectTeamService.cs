using cat_a_logB.Data;

namespace cat_a_logB.Service.Interfaces
{
    public interface IProjectTeamService
    {
        public void AddTeam(ProjectTeam projectTeam);

        public void RemoveTeam(ProjectTeam projectTeam);

        public void AddTeams(List<ProjectTeam> projectTeams);

        public void RemoveTeams(List<ProjectTeam> projectTeams);

        public List<ProjectTeam> GetAllTeams();
    }
}
