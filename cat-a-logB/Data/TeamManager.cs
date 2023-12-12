using ApexCharts;
using cat_a_logB.Pages;
using cat_a_logB.Service;
using Microsoft.AspNetCore.Components;

namespace cat_a_logB.Data
{
    public class TeamManager
    {
        private readonly IDependencyService dependencyService;
        private readonly ITaskDataService taskDataService;
        private readonly IProjectTeamService projectTeamService;
        private readonly IMilestoneService milestoneService;

        public TeamManager(IDependencyService _dependencyService, ITaskDataService _taskDataService, IProjectTeamService _projectTeamService, IMilestoneService _milestoneService)
        {
            dependencyService = _dependencyService;
            taskDataService = _taskDataService;
            projectTeamService = _projectTeamService;
            milestoneService = _milestoneService;
        }

        public TeamManager() { }

        private List<ProjectTeam> teams;
        public ProjectTeam newTeam { get; set; }
        public string errorMessage { get; private set; }

        public List<TaskData> tasks;

        public async Task AddTeam(List<ProjectTeam> teams, ProjectTeam newTeam)
        {
            if (string.IsNullOrWhiteSpace(newTeam.Name))
            {
                errorMessage = "Team name is required.";
                return;
            }

            errorMessage = "";

            projectTeamService.AddTeam(newTeam);
            teams.Add(newTeam);
            newTeam = new ProjectTeam(); // Reset the new team object after adding
            
        }

        public async Task RefreshData(List<ProjectTeam> teams, List<TaskData> tasks)
        {
            teams = projectTeamService.GetAllTeams();
            tasks = tasks = taskDataService.GetAllTasks();


        }
    }

}
