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
using System.Collections.Generic;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace cat_a_logB_UnitTests
{

    [TestFixture]
    public class EditTaskNameTests
    {
        [Test]
        public void EditTaskName_EmptyName_SetsErrorMessage()
        {
            // Arrange
            var project = new List<TaskData>
            {
                new TaskData { Name = "Task 1" },
                new TaskData { Name = "Task 2" }
                // Add more tasks if needed for testing
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

            var taskManager = new TaskManager();

            // Act
            taskManager.EditTaskName(project, chartMock, selectedData, onCloseCallbackMock, "");

            // Assert
            NUnit.Framework.Assert.AreEqual("Name cannot be empty.", taskManager.errorMessage);
        }
        [Test]
        public void EditTaskName_ExistingName_SetsErrorMessage()
        {
            // Arrange
            var project = new List<TaskData>
            {
                new TaskData { Name = "Task 1" },
                new TaskData { Name = "Task 2" }
                // Add more tasks if needed for testing
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

            var taskManager = new TaskManager();

            // Act
            taskManager.EditTaskName(project, chartMock, selectedData, onCloseCallbackMock, "Task 2");

            // Assert
            NUnit.Framework.Assert.AreEqual("Task name is already in use.", taskManager.errorMessage);
        }
        [Test]
        public void EditTaskName_ValidNameChange_NoErrorMessage()
        {
            // Arrange
            var project = new List<TaskData>
            {
                new TaskData { Name = "Task 1" },
                new TaskData { Name = "Task 2" }

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

            var taskManager = new TaskManager();

            // Act
            taskManager.EditTaskName(project, chartMock, selectedData, onCloseCallbackMock, "New Task");

            // Assert
            NUnit.Framework.Assert.IsEmpty(taskManager.errorMessage);


            var updatedTask = project.Find(task => task.Name == "New Task");
            NUnit.Framework.Assert.IsNotNull(updatedTask);
        }
        [Test]
        public void EditTaskName_NullOrInvalidSelectedData_NoUpdates()
        {
            // Arrange
            var project = new List<TaskData>
            {
                new TaskData { Name = "Task 1" },
                new TaskData { Name = "Task 2" }

            };

            var chartMock = new ApexChart<TaskData>();


            var taskManagerWithNullSelectedData = new TaskManager();
            //act
            taskManagerWithNullSelectedData.EditTaskName(project, chartMock, null, new EventCallback(), "New Task");

            NUnit.Framework.Assert.IsEmpty(taskManagerWithNullSelectedData.errorMessage);
            NUnit.Framework.Assert.AreEqual(2, project.Count);


            var selectedDataWithoutItems = new SelectedData<TaskData>();
            var taskManagerWithInvalidSelectedData = new TaskManager();
            taskManagerWithInvalidSelectedData.EditTaskName(project, chartMock, selectedDataWithoutItems, new EventCallback(), "New Task");

        
            NUnit.Framework.Assert.IsEmpty(taskManagerWithInvalidSelectedData.errorMessage);
            NUnit.Framework.Assert.AreEqual(2, project.Count);
        }
    }
}