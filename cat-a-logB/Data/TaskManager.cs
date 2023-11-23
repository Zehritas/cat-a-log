using ApexCharts;
using Microsoft.AspNetCore.Components;
using System.Globalization;
using static cat_a_logB.Data.ProjectMilestone;


namespace cat_a_logB.Data
{
    public class TaskManager
    {
        public List<GanttData> project;
        public ApexChart<GanttData> chart;
        public SelectedData<GanttData> selectedData;
        public EventCallback OnClose;
        public ApexChart<ProjectMilestone> mileChart;
        public List<ProjectMilestone> milestones;
        public string errorMessage { get; private set; }

        public string newTaskName { get; private set; }
        public DateTime newTaskStartTime { get; private set; }
        public DateTime newTaskEndTime { get; private set; }


        public async Task EditComments(List<GanttData> project, ApexChart<GanttData> chart, SelectedData<GanttData> selectedData, EventCallback OnClose, string editedComments)
        {
            if (selectedData != null && selectedData.DataPoint != null
             && selectedData.DataPoint.Items.First().Name is string selectedTaskName)
            {
                GanttData taskToUpdate = project.FirstOrDefault(task => task.Name == selectedTaskName);
                if (taskToUpdate != null)
                {
                    taskToUpdate.Comments = editedComments;
                }

                OnClose.InvokeAsync();
                await chart.UpdateSeriesAsync();
            }
            else
            {
                Console.WriteLine("Aaa");
            }
        }

        public async Task EditTaskProgress(ApexChart<ProjectMilestone> mileChart, List<GanttData> project, ApexChart<GanttData> chart, SelectedData<GanttData> selectedData, EventCallback OnClose, List<ProjectMilestone> milestones, int progressValue)
        {
            if (selectedData != null && selectedData.DataPoint != null &&
                selectedData.DataPoint.Items.First().Name is string selectedTaskName)
            {
                GanttData taskToUpdate = project.FirstOrDefault(task => task.Name == selectedTaskName);
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
                                milestone.Color = "green"; 
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


        public async Task EditTaskTime(List<GanttData> project, ApexChart<GanttData> chart, SelectedData<GanttData> selectedData, EventCallback OnClose, DateTime newStartDate, DateTime newEndDate)
        {
            if (newStartDate >= newEndDate)
            {
                errorMessage = "Invalid. Start date must not be higher than or equal to end date.";
                return;
            }

            errorMessage = "";
            if (selectedData != null && selectedData.DataPoint != null &&
                    selectedData.DataPoint.Items.First().Name is string selectedTaskName)
                {
                    
                    // Find the task in the project list with the matching name and update its StartDate and EndDate properties
                    GanttData taskToUpdate = project.FirstOrDefault(task => task.Name == selectedTaskName);
                    if (taskToUpdate != null)
                    {
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

        public async Task EditTaskName(List<GanttData> project, ApexChart<GanttData> chart, SelectedData<GanttData> selectedData, EventCallback OnClose, string newTaskName) // Strictly to edit the name and refresh
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

            errorMessage = "";
            if (selectedData != null && selectedData.DataPoint != null &&
            selectedData.DataPoint.Items.First().Name is string selectedTaskName)
            {
                GanttData taskToUpdate = project.FirstOrDefault(task => task.Name == selectedTaskName);
                if (taskToUpdate != null)
                {
                    taskToUpdate.Name = newTaskName;
                }
            }
            else
            {

            }
            newTaskName = "";
            await chart.UpdateSeriesAsync();
            OnClose.InvokeAsync();
        }


    }
}