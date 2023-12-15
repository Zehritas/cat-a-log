using ApexCharts;
using cat_a_logB.Pages;
using cat_a_logB.Service;
using cat_a_logB.Data;
using Microsoft.AspNetCore.Components;
using cat_a_logB.Service.Interfaces;



namespace cat_a_logB.Data
{
    public class CalculationData
    {
        private readonly IDependencyService dependencyService;
        private readonly ITaskDataService taskDataService;
        private readonly IProjectTeamService projectTeamService;
        private readonly IMilestoneService milestoneService;

        public CalculationData(IDependencyService _dependencyService, ITaskDataService _taskDataService, IProjectTeamService _projectTeamService, IMilestoneService _milestoneService)
        {
            dependencyService = _dependencyService;
            taskDataService = _taskDataService;
            projectTeamService = _projectTeamService;
            milestoneService = _milestoneService;
        }
        public CalculationData() { }

        

        public string CheckEstimatedProgress(TaskData task)
        {
            if (task.Progress == 100)
            {
                return "Task " + task.Name + " is already complete.";
            }
            else
            {
                double remainingDays = (task.EndDate - DateTime.Now).TotalDays;

                if (task.Progress >= task.AutoProgress)
                {
                    return "Task " + task.Name + " should be finished on time.";
                }
                else
                {
                    var team = task.Team;
                    int additionalPeopleNeeded = CalculateAdditionalPeople(task, team);
                    return "Task " + task.Name + " is behind schedule. Consider adding " + additionalPeopleNeeded + " more people.";
                }
            }
        }

        public int CalculateAdditionalPeople(TaskData task, ProjectTeam team)
        {
            double progressPerPerson = task.Progress / team.TeamMembers.Count;
            double progressWithoutOriginalPeople = (100 / task.AutoProgress) * task.Progress;
            double remainingTime = 100 - task.AutoProgress;

            // Assuming all team members contribute equally, calculate the number of additional people needed
            int additionalPeopleNeeded = (int)Math.Ceiling((100 - progressWithoutOriginalPeople) / (remainingTime / task.AutoProgress * progressPerPerson));

            return additionalPeopleNeeded;
        }

        public string CompareUserProgress(TaskData task)
        {
            double userProgress = task.Progress;
            double taskTimeProgress = task.AutoProgress;
            double userDaysAheadOrBehind = (taskTimeProgress - userProgress) * (task.EndDate - task.StartDate).TotalDays / 100.0;

            if (userProgress == 100)
            {
                return "Task completed.";
            }
            else if (userDaysAheadOrBehind > 0)
            {
                return $"User is {userDaysAheadOrBehind:N1} days behind.";
            }
            else if (userDaysAheadOrBehind < 0) { return $"User is {Math.Abs(userDaysAheadOrBehind):N1} days ahead."; }
            else
            {
                return "User is on track.";
            }
        }
        public double CalculateAutoProgress(TaskData task)
        {
            var now = DateTime.Now;
            double totalDays = (task.EndDate - task.StartDate).TotalDays;
            double dayProgress;

            if (task.StartDate <= now && now <= task.EndDate)
            {
                double daysPassed = (now - task.StartDate).TotalDays;
                dayProgress = (daysPassed / totalDays) * 100;
            }
            else if (now > task.EndDate)
            {
                dayProgress = 100;
            }
            else
            {
                dayProgress = 0;
            }

            return dayProgress;
        }




    }
}
