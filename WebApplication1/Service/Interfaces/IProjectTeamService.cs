using Cat_a_logAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace Cat_a_logAPI.Service.Interfaces
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
