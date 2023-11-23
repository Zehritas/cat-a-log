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
    public class EditCommentsTest
    {
        [Test]
        public async Task EditComments_ValidCommentUpdate()
        {
            // Arrange
            var project = new List<TaskData>
            {
                new TaskData { Name = "Task 1", Comments = "Initial comment" },
                new TaskData { Name = "Task 2", Comments = "Initial comment" }

            };

            var chartMock = new ApexChart<TaskData>();
            var selectedData = new SelectedData<TaskData>
            {
                DataPoint = new DataPoint<TaskData>
                {
                    Items = new List<TaskData> { new TaskData { Name = "Task 1" } }
                }
            };

            var onCloseCallbackMock = new EventCallback();
            var editedComments = "Updated comment";

            var taskManager = new TaskManager();

            // Act
            taskManager.EditComments(project, chartMock, selectedData, onCloseCallbackMock, editedComments);

            // Assert
            var updatedTask = project.Find(task => task.Name == "Task 1");
            NUnit.Framework.Assert.IsNotNull(updatedTask);
            NUnit.Framework.Assert.AreEqual("Updated comment", updatedTask.Comments);
        }
        [Test]
        public async Task EditComments_InvalidSelectedData_NoUpdate()
        {
            // Arrange
            var project = new List<TaskData>
                {
                    new TaskData { Name = "Task 1", Comments = "Initial comment" }

                };

            var chartMock = new ApexChart<TaskData>();


            SelectedData<TaskData> invalidSelectedData = null;

            var onCloseCallbackMock = new EventCallback();
            var editedComments = "Updated comment";

            var taskManager = new TaskManager();

            // Act
            await taskManager.EditComments(project, chartMock, invalidSelectedData, onCloseCallbackMock, editedComments);

            // Assert
            var updatedTask = project.FirstOrDefault(task => task.Name == "Task 1");
            NUnit.Framework.Assert.IsNotNull(updatedTask);
            NUnit.Framework.Assert.AreEqual("Initial comment", updatedTask.Comments);
        }
        [Test]
        public async Task EditComments_TaskNotFound_NoUpdate()
        {
            // Arrange
            var project = new List<TaskData>
            {
                new TaskData { Name = "Task 1", Comments = "Initial comment" }

            };

            var chartMock = new ApexChart<TaskData>();

            var selectedData = new SelectedData<TaskData>
            {
                DataPoint = new DataPoint<TaskData>
                {
                    Items = new List<TaskData> { new TaskData { Name = "Nonexistent Task" } }
                }
            };

            var onCloseCallbackMock = new EventCallback();
            var editedComments = "Updated comment";

            var taskManager = new TaskManager();

            // Act
            taskManager.EditComments(project, chartMock, selectedData, onCloseCallbackMock, editedComments);

            // Assert
            var updatedTask = project.FirstOrDefault(task => task.Name == "Task 1");
            NUnit.Framework.Assert.IsNotNull(updatedTask);
            NUnit.Framework.Assert.AreEqual("Initial comment", updatedTask.Comments);
        }
    }
}