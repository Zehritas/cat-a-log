using ApexCharts;
using cat_a_logB.Service;
using Microsoft.AspNetCore.Components;
using static cat_a_logB.Data.ProjectMilestone;



namespace cat_a_logB.Data
{
    public class TaskManager
    {
        private readonly IDependencyService dependencyService;
        private readonly ITaskDataService taskDataService;
        private readonly IProjectTeamService projectTeamService;
        private readonly IMilestoneService milestoneService;

        public TaskManager(IDependencyService _dependencyService, ITaskDataService _taskDataService, IProjectTeamService _projectTeamService, IMilestoneService _milestoneService)
        {
            dependencyService = _dependencyService;
            taskDataService = _taskDataService;
            projectTeamService = _projectTeamService;
            milestoneService = _milestoneService;
        }

        public TaskManager(){ }

        public List<TaskData> project;
        public ApexChart<TaskData> chart;
        public SelectedData<TaskData> selectedData;
        public EventCallback OnClose;
        public ApexChart<ProjectMilestone> mileChart;
        public List<ProjectMilestone> milestones;

        public string errorMessage { get; private set; }
        public string newTaskName { get; private set; }
        public DateTime newTaskStartTime { get; private set; }
        public DateTime newTaskEndTime { get; private set; }

        public async Task EditComments(List<TaskData> project, ApexChart<TaskData> chart, SelectedData<TaskData> selectedData, EventCallback OnClose, string editedComments)
        {
            if (selectedData != null && selectedData.DataPoint != null
             && selectedData.DataPoint.Items.First().Id is int selectedTaskId)
            {
                TaskData taskToUpdate = project.FirstOrDefault(task => task.Id == selectedTaskId);
                if (taskToUpdate != null)
                {
                    taskDataService.ChangeTaskComment(taskToUpdate.Id, editedComments);
                    //taskToUpdate.Comments = editedComments;
                }


                OnClose.InvokeAsync();
                await chart.UpdateSeriesAsync();
            }
            else
            {
                Console.WriteLine("Aaa");
            }
        }

        public async Task EditTaskProgress(ApexChart<ProjectMilestone> mileChart, List<TaskData> project, ApexChart<TaskData> chart, SelectedData<TaskData> selectedData, EventCallback OnClose, List<ProjectMilestone> milestones, int progressValue)
        {
            if (selectedData != null && selectedData.DataPoint != null &&
                selectedData.DataPoint.Items.First().Id is int selectedTaskId)
            {
                TaskData taskToUpdate = project.FirstOrDefault(task => task.Id == selectedTaskId);
                if (taskToUpdate != null)
                {
                    taskToUpdate.Progress = progressValue;

                    if (progressValue == 100)
                    {
                        taskToUpdate.PointColor = "#CCCCCC";
                        foreach (var milestone in milestones)
                        {
                            TaskCompletionStatus completionStatus = milestone.GetTaskCompletionStatus();
                            if (completionStatus == TaskCompletionStatus.Completed)
                            {
                                milestoneService.ChangeMilestoneColor(milestone.Id, "green");
                                //milestone.Color = "green";
                                await mileChart.RenderAsync();
                            }
                        }
                    }
                    else
                    {
                        taskToUpdate.PointColor = taskToUpdate.Team.Color;
                    }
                }
            }
            await chart.UpdateSeriesAsync();
            OnClose.InvokeAsync();
        }


        public async Task EditTaskTime(List<TaskData> project, ApexChart<TaskData> chart, SelectedData<TaskData> selectedData, EventCallback OnClose, DateTime newStartDate, DateTime newEndDate)
        {
            if (newStartDate >= newEndDate)
            {
                errorMessage = "Invalid. Start date must not be higher than or equal to end date.";
                return;
            }

            errorMessage = "";
            if (selectedData != null && selectedData.DataPoint != null &&
                    selectedData.DataPoint.Items.First().Id is int selectedTaskId)
            {

                
                TaskData taskToUpdate = project.FirstOrDefault(task => task.Id == selectedTaskId);
                if (taskToUpdate != null)
                {
                    taskDataService.ChangeTaskStartDate(taskToUpdate.Id, newStartDate);
                    taskDataService.ChangeTaskEndDate(taskToUpdate.Id, newEndDate);
                    taskToUpdate.StartDate = newStartDate;
                    taskToUpdate.EndDate = newEndDate;
                }
            }
            else
            {
                Console.WriteLine("SelectedData is null");
            }

            await chart.UpdateSeriesAsync();
            OnClose.InvokeAsync();

        }

        public async Task EditTaskName(List<TaskData> project, ApexChart<TaskData> chart, SelectedData<TaskData> selectedData, EventCallback OnClose, string newTaskName) // Strictly to edit the name and refresh
        {
            if (string.IsNullOrWhiteSpace(newTaskName))
            {
                errorMessage = "Name cannot be empty.";
                return;
            }

            if (project.Any(task => task.Name == newTaskName))
            {
                errorMessage = "Task name is already in use.";
                return;
            }

            if (!ValidationExtensions.IsValidTaskName(newTaskName))
            {
                errorMessage = "Invalid task name. Only alphanumeric characters and spaces are allowed.";
                return;
            }

            errorMessage = "";
            if (selectedData != null && selectedData.DataPoint != null &&
            selectedData.DataPoint.Items.First().Id is int selectedTaskId)
            {
                TaskData taskToUpdate = project.FirstOrDefault(task => task.Id == selectedTaskId);
                if (taskToUpdate != null)
                {
                    taskDataService.ChangeTaskName(taskToUpdate.Id, newTaskName);
                    //taskToUpdate.Name = newTaskName;
                }
            }
            else
            {

            }
            newTaskName = "";
            await chart.UpdateSeriesAsync();
            OnClose.InvokeAsync();
        }

        public async Task Reschedule(TaskData predecessorTask, List<TaskData> tasks, ApexChart<TaskData> chart)
        {
            foreach (var dependency in predecessorTask.Dependencies)
            {
                TaskData successorTask = tasks.FirstOrDefault(task => task.Id == dependency.SuccessorTaskId);

                if (successorTask == null)
                {
                    // Handle the case where the successor task is not found
                    throw new InvalidOperationException($"Task not found for dependency: {dependency.SuccessorTaskId}");
                }
                double successorTaskLength = (successorTask.EndDate - successorTask.StartDate).TotalDays;


                switch (dependency.Type)
                {
                    case DependencyType.FS:
                        if (predecessorTask.EndDate > successorTask.StartDate)
                        {
                            taskDataService.ChangeTaskStartDate(successorTask.Id, predecessorTask.EndDate);
                            taskDataService.UpdateTask(successorTask);
                            taskDataService.ChangeTaskEndDate(successorTask.Id, successorTask.StartDate.AddDays(successorTaskLength));
                            //successorTask.StartDate = predecessorTask.EndDate;
                            //successorTask.EndDate = successorTask.StartDate.AddDays(successorTaskLength);

                            taskDataService.UpdateTask(successorTask);
                            await Reschedule(successorTask, tasks, chart);
                            await chart.UpdateSeriesAsync();
                            // StateHasChanged();
                        }
                        break;

                    case DependencyType.SF:
                        if (predecessorTask.EndDate < successorTask.StartDate)
                        {
                            taskDataService.ChangeTaskStartDate(successorTask.Id, predecessorTask.EndDate);
                            taskDataService.ChangeTaskEndDate(successorTask.Id, successorTask.StartDate.AddDays(successorTaskLength));
                            //successorTask.StartDate = predecessorTask.EndDate;
                            //successorTask.EndDate = successorTask.StartDate.AddDays(successorTaskLength);
                            taskDataService.UpdateTask(successorTask);
                            await Reschedule(successorTask, tasks, chart);
                            await chart.UpdateSeriesAsync();
                            // StateHasChanged();
                        }
                        break;

                    case DependencyType.SS:
                        if (predecessorTask.StartDate > successorTask.StartDate)
                        {
                            taskDataService.ChangeTaskStartDate(successorTask.Id, predecessorTask.StartDate);
                            taskDataService.ChangeTaskEndDate(successorTask.Id, successorTask.StartDate.AddDays(successorTaskLength));
                            //successorTask.StartDate = predecessorTask.StartDate;
                            //successorTask.EndDate = successorTask.StartDate.AddDays(successorTaskLength);

                            taskDataService.UpdateTask(successorTask);
                            await Reschedule(successorTask, tasks, chart);
                            await chart.UpdateSeriesAsync();
                            // StateHasChanged();
                        }
                        break;

                    case DependencyType.FF:
                        if (predecessorTask.EndDate > successorTask.EndDate)
                        {
                            taskDataService.ChangeTaskStartDate(successorTask.Id, successorTask.EndDate.AddDays(-successorTaskLength));
                            taskDataService.ChangeTaskEndDate(successorTask.Id, predecessorTask.EndDate);
                            //successorTask.EndDate = predecessorTask.EndDate;
                            //successorTask.StartDate = successorTask.EndDate.AddDays(-successorTaskLength);

                            taskDataService.UpdateTask(successorTask);
                            await Reschedule(successorTask, tasks, chart);
                            await chart.UpdateSeriesAsync();
                            // StateHasChanged();
                        }
                        break;

                    default:
                        // Handle unsupported dependency types
                        throw new ArgumentException("Unsupported DependencyType");
                }
            }

        }
    }
}