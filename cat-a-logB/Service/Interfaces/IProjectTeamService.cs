using cat_a_logB.Data;

namespace cat_a_logB.Service.Interfaces
{
    public interface IProjectTeamService
    {
        public bool AddTeam(ProjectTeam projectTeam);

        public bool RemoveTeam(int id);

        public bool AddTeams(List<ProjectTeam> projectTeams);

        public bool RemoveTeams(List<ProjectTeam> projectTeams);

        public ProjectTeam GetTeam(int Id);

        public List<ProjectTeam> GetTeams();

        public bool UpdateTeam(ProjectTeam team);

        public bool TeamExists(int id);
    }
}
