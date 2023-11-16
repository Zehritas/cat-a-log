using ApexCharts;
using Microsoft.AspNetCore.Components;


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
                            milestone.CalculateCompletedTasksPercentage();
                            if (milestone.Color == "green")
                            {
                                mileChart.RenderAsync();
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
                //Console.WriteLine("SelectedData is null somehow");
            }

            await chart.UpdateSeriesAsync();
            OnClose.InvokeAsync();
        }

        public async Task EditTaskName(List<GanttData> project, ApexChart<GanttData> chart, SelectedData<GanttData> selectedData, EventCallback OnClose, string newTaskName) // Strictly to edit the name and refresh
        {
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