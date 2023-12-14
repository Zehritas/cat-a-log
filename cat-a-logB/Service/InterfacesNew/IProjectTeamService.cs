using cat_a_logB.Data;

namespace cat_a_logB.Service.InterfacesNew
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
