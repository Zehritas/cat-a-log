using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using ApexCharts;
using cat_a_logB.Data;
using cat_a_logB.Pages;
using Microsoft.AspNetCore.Components;
using NuGet.Protocol.Plugins;
using NUnit.Framework;

namespace cat_a_logB_UnitTests
{
    public class EditTaskProgressTests
    {

        [Test]
        public async Task EditTaskProgress_ValidProgressChange()
        {
            // Arrange
            var project = new List<GanttData>
                {
                    new GanttData { Name = "Task 1", Progress = 0 }, 
                };

            var milestones = new List<ProjectMilestone>
                {
                    new ProjectMilestone("Milestone 1", project, DateTime.Today, "green"),
        
                 };

            var selectedData = new SelectedData<GanttData>
            {
                DataPoint = new DataPoint<GanttData>
                {
                    Items = new List<GanttData> { new GanttData { Name = "Task 1" } }
                }
            };

            var chartMock = new ApexChart<GanttData>();
            var mileChartMock = new ApexChart<ProjectMilestone>();
            var onCloseCallbackMock = new EventCallback();

            var taskManager = new TaskManager();

            // Act
            taskManager.EditTaskProgress(mileChartMock, project, chartMock, selectedData, onCloseCallbackMock, milestones, 50); 

            // Assert
            var updatedTask = project.FirstOrDefault(task => task.Name == "Task 1");
            NUnit.Framework.Assert.IsNotNull(updatedTask); 

            NUnit.Framework.Assert.AreEqual(50, updatedTask.Progress); 
        }
        [Test]
        public async Task EditTaskProgress_ProgressCompletionHandling()
        {
            // Arrange
            var project = new List<GanttData>
                {
                    new GanttData { Name = "Task 1", Progress = 50, PointColor = "#123456" }
        
                };

            var chartMock = new ApexChart<GanttData>();

            var selectedData = new SelectedData<GanttData>
            {
                DataPoint = new DataPoint<GanttData>
                {
                    Items = new List<GanttData> { new GanttData { Name = "Task 1" } }
                }
            };

            var milestones = new List<ProjectMilestone>
                {
                    new ProjectMilestone("Milestone 1", new List<GanttData>(), DateTime.Today, "red"),
                    new ProjectMilestone("Milestone 2", new List<GanttData>(), DateTime.Today.AddDays(1), "blue")
        
                };

            var mileChartMock = new ApexChart<ProjectMilestone>();
            var onCloseCallbackMock = new EventCallback();
            var taskManager = new TaskManager();

            // Act
            taskManager.EditTaskProgress(mileChartMock, project, chartMock, selectedData, onCloseCallbackMock, milestones, 100);

            // Assert
            var updatedTask = project.FirstOrDefault(task => task.Name == "Task 1");
            NUnit.Framework.Assert.AreEqual("#CCCCCC", updatedTask.PointColor);
            NUnit.Framework.Assert.AreEqual("green", milestones[0].Color); 
        }
    }
}
