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
    [TestFixture]
    public class EditTaskTimeTests
    {
        [Fact]
        public async Task EditTaskTime_ValidDateChange()
        {
            // Arrange
            var project = new List<GanttData>
                {
                    new GanttData { Name = "Task 1", StartDate = DateTime.Today, EndDate = DateTime.Today.AddDays(1) }

                };


            var chartMock = new ApexChart<GanttData>();
            var selectedData = new SelectedData<GanttData>
            {
                DataPoint = new DataPoint<GanttData>
                {
                    Items = new List<GanttData> { new GanttData { Name = "Task 1" } }
                }
            };

            var onCloseCallbackMock = new EventCallback();
            var newStartDate = DateTime.Today.AddDays(2); // New valid start date
            var newEndDate = DateTime.Today.AddDays(5);   // New valid end date

            var taskManager = new TaskManager();

            // Act
            taskManager.EditTaskTime(project, chartMock, selectedData, onCloseCallbackMock, newStartDate, newEndDate);

            // Assert
            var updatedTask = project.FirstOrDefault(task => task.Name == "Task 1");
            NUnit.Framework.Assert.NotNull(updatedTask); // Ensure the task is found

            NUnit.Framework.Assert.AreEqual(newStartDate, updatedTask.StartDate); // Confirm start date updated
            NUnit.Framework.Assert.AreEqual(newEndDate, updatedTask.EndDate);     // Confirm end date updated
        }
        [Test]
        public async Task EditTaskTime_EndDateGreaterThanStartDate_ErrorSetNoUpdate()
        {
            // Arrange
            var project = new List<GanttData>
                {
                    new GanttData { Name = "Task 1", StartDate = DateTime.Today, EndDate = DateTime.Today.AddDays(1) }

                };

            var selectedData = new SelectedData<GanttData>
            {
                DataPoint = new DataPoint<GanttData>
                {
                    Items = new List<GanttData> { new GanttData { Name = "Task 1" } }
                }
            };


            var chartMock = new ApexChart<GanttData>();
            var onCloseCallbackMock = new EventCallback();

            var newStartDate = DateTime.Today.AddDays(2);
            var newEndDate = DateTime.Today.AddDays(1);

            var taskManager = new TaskManager();

            // Act
            taskManager.EditTaskTime(project, chartMock, selectedData, onCloseCallbackMock, newStartDate, newEndDate);

            // Assert
            NUnit.Framework.Assert.AreEqual("Invalid. Start date must not be higher than or equal to end date.", taskManager.errorMessage);
            var updatedTask = project.FirstOrDefault(task => task.Name == "Task 1");
            NUnit.Framework.Assert.IsNotNull(updatedTask);

            NUnit.Framework.Assert.AreEqual(DateTime.Today, updatedTask.StartDate);
            NUnit.Framework.Assert.AreEqual(DateTime.Today.AddDays(1), updatedTask.EndDate);
        }
        [Test]
        public async Task EditTaskTime_SelectedDataNotAvailable_NoUpdate()
        {
            // Arrange
            var project = new List<GanttData>
                {
                    new GanttData { Name = "Task 1", StartDate = DateTime.Today, EndDate = DateTime.Today.AddDays(1) }

                };

            var selectedData = new SelectedData<GanttData>
            {
                DataPoint = new DataPoint<GanttData>
                {
                    Items = new List<GanttData> { new GanttData { Name = "Task 1" } }
                }
            };
            SelectedData<GanttData> invalidSelectedData = null;
            var chartMock = new ApexChart<GanttData>();
            var onCloseCallbackMock = new EventCallback();
            var taskManager = new TaskManager();
            var newStartDate = DateTime.Today.AddDays(2);
            var newEndDate = DateTime.Today.AddDays(5);

            // Act
            taskManager.EditTaskTime(project, chartMock, invalidSelectedData, onCloseCallbackMock, newStartDate, newEndDate);

            //Assert
            NUnit.Framework.Assert.IsEmpty(taskManager.errorMessage);


            var updatedTask = project.FirstOrDefault(task => task.Name == "Task 1");
            NUnit.Framework.Assert.IsNotNull(updatedTask);


            NUnit.Framework.Assert.AreEqual(DateTime.Today, updatedTask.StartDate);
            NUnit.Framework.Assert.AreEqual(DateTime.Today.AddDays(1), updatedTask.EndDate);

        }
        [Test]
        public async Task EditTaskTime_TaskToUpdateNotFound_NoUpdate()
        {
            // Arrange
            var project = new List<GanttData>
                {
                    new GanttData { Name = "Task 1", StartDate = DateTime.Today, EndDate = DateTime.Today.AddDays(1) }

                };

            var selectedData = new SelectedData<GanttData>
            {
                DataPoint = new DataPoint<GanttData>
                {
                    Items = new List<GanttData> { new GanttData { Name = "Nonexistent Task" } }
                }
            };
            SelectedData<GanttData> invalidSelectedData = null;
            var chartMock = new ApexChart<GanttData>();
            var onCloseCallbackMock = new EventCallback();
            var taskManager = new TaskManager();
            var newStartDate = DateTime.Today.AddDays(2);
            var newEndDate = DateTime.Today.AddDays(5);

            // Act
            taskManager.EditTaskTime(project, chartMock, selectedData, onCloseCallbackMock, newStartDate, newEndDate);

            // Assert
            NUnit.Framework.Assert.IsEmpty(taskManager.errorMessage);

            
            var updatedTask = project.FirstOrDefault(task => task.Name == "Task 1");
            NUnit.Framework.Assert.IsNotNull(updatedTask); // Ensure the task still exists

            
            NUnit.Framework.Assert.AreEqual(DateTime.Today, updatedTask.StartDate);
            NUnit.Framework.Assert.AreEqual(DateTime.Today.AddDays(1), updatedTask.EndDate);
        }
        [Test]
        public async Task EditTaskTime_NullChartParameter_NoUpdate()
        {
            // Arrange
            var project = new List<GanttData>
                {
                    new GanttData { Name = "Task 1", StartDate = DateTime.Today, EndDate = DateTime.Today.AddDays(1) }

                };

            var selectedData = new SelectedData<GanttData>
            {
                DataPoint = new DataPoint<GanttData>
                {
                    Items = new List<GanttData> { new GanttData { Name = "Nonexistent Task" } }
                }
            };
            SelectedData<GanttData> invalidSelectedData = null;
            ApexChart<GanttData> nullChart = null;
            var onCloseCallbackMock = new EventCallback();
            var taskManager = new TaskManager();
            var newStartDate = DateTime.Today.AddDays(2);
            var newEndDate = DateTime.Today.AddDays(5);

            //Act
            taskManager.EditTaskTime(project, nullChart, selectedData, onCloseCallbackMock, newStartDate, newEndDate);

            // Assert
            NUnit.Framework.Assert.IsEmpty(taskManager.errorMessage);

            
            var updatedTask = project.FirstOrDefault(task => task.Name == "Task 1");
            NUnit.Framework.Assert.IsNotNull(updatedTask); // Ensure the task still exists

            
            NUnit.Framework.Assert.AreEqual(DateTime.Today, updatedTask.StartDate);
            NUnit.Framework.Assert.AreEqual(DateTime.Today.AddDays(1), updatedTask.EndDate);
        }
    }
}