using ApexCharts;
using cat_a_logB.Pages;
using cat_a_logB.Service;
using cat_a_logB.Service.Interfaces;
using Microsoft.AspNetCore.Components;


namespace cat_a_logB.Data
{
    public class MilestoneManager
    {
        private readonly IDependencyService dependencyService;
        private readonly ITaskDataService taskDataService;
        private readonly IProjectTeamService projectTeamService;
        private readonly IMilestoneService milestoneService;

        public MilestoneManager(IDependencyService _dependencyService, ITaskDataService _taskDataService, IProjectTeamService _projectTeamService, IMilestoneService _milestoneService)
        {
            dependencyService = _dependencyService;
            taskDataService = _taskDataService;
            projectTeamService = _projectTeamService;
            milestoneService = _milestoneService;
        }

        public MilestoneManager() { }

        public ProjectMilestone clickedMilestone;
        public List<TaskData> project;
        public ApexChart<TaskData> chart;
        private List<ProjectMilestone> displayedMilestones;
        public string milestoneName { get; set; }
        private List<TaskData> selectedTasks;
        public List<ProjectMilestone> milestones;

        private ProjectMilestone newMilestone { get; set; }

        public DateTime milestoneDate { get; set; }
        public string errorMessage { get; private set; }
        public async Task ShowMilestone(ApexChart<TaskData> chart, SelectedData<ProjectMilestone> data, ProjectMilestone clickedMilestone, List<ProjectMilestone> displayedMilestones)
        {
            clickedMilestone = data?.DataPoint?.Items?.FirstOrDefault();

            if (clickedMilestone != null)
            {
                if (displayedMilestones.Contains(clickedMilestone))
                {
                    displayedMilestones.Remove(clickedMilestone);
                    await RedrawAnnotations(chart, displayedMilestones);
                }
                else
                {
                    displayedMilestones.Add(clickedMilestone);
                    await RedrawAnnotations(chart, displayedMilestones);
                }
            }
        }


        public async Task RedrawAnnotations(ApexChart<TaskData> chart, List<ProjectMilestone> displayedMilestones)
        {
            await chart.ClearAnnotationsAsync();

            foreach (ProjectMilestone milestone in displayedMilestones)
            {
                await AddMilestoneAnnotation(milestone, chart);
            }
        }



        private async Task AddMilestoneAnnotation(ProjectMilestone milestone, ApexChart<TaskData> chart)
        {
            String XMValue = milestone.TargetDate.ToUnixTimeMilliseconds().ToString();
            String tasksText = string.Join(" ", milestone.Tasks.Select(t =>
            {
                String progressText = (t.Progress == 100) ? "(Complete)" : "";
                return $"{t.Name}{progressText}";
            }));

            AnnotationsPoint point = new AnnotationsPoint
            {
                X = XMValue,
                Y = 0,
                Label = new Label
                {
                    Text = $"{milestone.Name}: {tasksText}",
                    Style = new Style
                    {
                        FontSize = "17px",
                        Padding = new ApexCharts.Padding { Left = 3, Right = 3, Top = 20, Bottom = 20 },
                    },
                },
            };

            AnnotationsXAxis point2 = new AnnotationsXAxis
            {
                X = XMValue,
                StrokeDashArray = 10,
                BorderWidth = 3,
            };

            await chart.AddXAxisAnnotationAsync(point2, false);
            await chart.AddPointAnnotationAsync(point, false);

        }
        public async void CreateMilestone(List<TaskData> selectedTasks, List<ProjectMilestone> milestones, ProjectMilestone newMilestone, string milestoneName, DateTime milestoneDate)
        {

            errorMessage = ""; // Clear any previous error message

            if (string.IsNullOrWhiteSpace(milestoneName))
            {
                errorMessage = "Milestone name is required.";
                return;
            }

            if (milestones.Any(m => m.Name.Equals(milestoneName, StringComparison.OrdinalIgnoreCase)))
            {
                errorMessage = "Milestone name already exists.";
                return;
            }

            if (selectedTasks == null || !selectedTasks.Any())
            {
                errorMessage = "Please select at least one task.";
                return;
            }


            newMilestone = new ProjectMilestone(milestoneName, selectedTasks.ToList(), milestoneDate, "blue");

            milestoneService.AddMilestone(newMilestone);
            milestones.Add(newMilestone);
            newMilestone = new ProjectMilestone();
            milestoneName = string.Empty;
            selectedTasks.Clear();
        }


    }
}
