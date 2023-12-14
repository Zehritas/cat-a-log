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

namespace cat_a_logTests
{
    [TestFixture]
    public class RemoveTaskTests
    {
        [Test]
        public async Task RemoveTask_ValidInput_TaskRemoved()
        {

            var project = new List<TaskData>
    {
        new TaskData { Id = 1, Name = "Task 1" },
        new TaskData { Id = 2, Name = "Task 2" },

    };

            var chart = new ApexChart<TaskData>();
            var selectedData = new SelectedData<TaskData>
            {
                DataPoint = new DataPoint<TaskData>
                {
                    Items = new List<TaskData>
            {
                new TaskData { Id = 1 }
            }
                }
            };

            var taskManager = new TaskManager();

            // Act
            taskManager.RemoveTask(selectedData, project, chart);

            // Assert
            NUnit.Framework.Assert.IsFalse(project.Any(task => task.Id == 1)); // Task with ID 1 should be removed
                                                               // Add assertions to check chart updates after task removal
        }
        [Test]
        public async Task RemoveTask_NullSelectedData_NoTaskRemoved()
        {
            // Arrange
            var project = new List<TaskData>
    {
        new TaskData { Id = 1, Name = "Task 1" },
        new TaskData { Id = 2, Name = "Task 2" },
        // Add more tasks as needed
    };

            var chart = new ApexChart<TaskData>();
            SelectedData<TaskData> selectedData = null; // Null selected data

            var taskManager = new TaskManager();

            // Act
            taskManager.RemoveTask(selectedData, project, chart);

            // Assert
            // Ensure no task is removed when selected data is null
            NUnit.Framework.Assert.AreEqual(2, project.Count); // The project should remain unchanged
                                               // Add assertions to check chart remains unchanged
        }

        [Test]
        public async Task RemoveTask_InvalidSelectedData_NoTaskRemoved()
        {
            // Arrange
            var project = new List<TaskData>
    {
        new TaskData { Id = 1, Name = "Task 1" },
        new TaskData { Id = 2, Name = "Task 2" },
        // Add more tasks as needed
    };

            var chart = new ApexChart<TaskData>();
            var selectedData = new SelectedData<TaskData>
            {
                DataPoint = new DataPoint<TaskData>
                {
                    // No items in selected data
                    Items = new List<TaskData>()
                }
            };

            var taskManager = new TaskManager();

            // Act
            taskManager.RemoveTask(selectedData, project, chart);

            // Assert
            // Ensure no task is removed when selected data is invalid (empty in this case)
            NUnit.Framework.Assert.AreEqual(2, project.Count); // The project should remain unchanged
                                               // Add assertions to check chart remains unchanged
        }
        

       
    }
}
