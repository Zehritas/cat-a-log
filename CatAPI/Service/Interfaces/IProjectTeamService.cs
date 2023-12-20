using Cat_a_logAPI.Data;

namespace Cat_a_logAPI.Service.Interfaces
{
    public interface IProjectTeamService
    {
        public bool AddTeam(ProjectTeam projectTeam);

        public bool RemoveTeam(int id);

        public bool AddTeams(IEnumerable<ProjectTeam> projectTeams);

        public bool RemoveTeams(IEnumerable<ProjectTeam> projectTeams);

        public ProjectTeam GetTeam(int Id);

        public IEnumerable<ProjectTeam> GetTeams();

        public bool UpdateTeam(ProjectTeam team);

        public bool TeamExists(int id);
    }
}
