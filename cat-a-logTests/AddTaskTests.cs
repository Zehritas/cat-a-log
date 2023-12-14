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
    public class AddTaskTests
    {
        [Test]
        public async Task AddTask_ValidInput_TaskAddedToProjectAndChartUpdated()
        {
            // Arrange
            var project = new List<TaskData>();
            var chart = new ApexChart<TaskData>();
            var teams = new List<ProjectTeam> {   
                new ProjectTeam { Name = "TeamA" },
                new ProjectTeam { Name = "TeamB" }, };
            var selectedTeamName = "TeamA";
            var newTask = new TaskData
            {
                Name = "New Task",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(5),
                Comments = "Some comments..."
            };

            var taskManager = new TaskManager();

            // Act
            taskManager.AddTask(project, chart, newTask, teams, selectedTeamName);

            // Assert
            NUnit.Framework.Assert.IsEmpty(taskManager.errorMessage); 
            NUnit.Framework.Assert.IsTrue(project.Any(task => task.Name == "New Task")); 
                                                                        
        }

        [Test]
        public async Task AddTask_ValidInput_ValidTaskName()
        {
            // Arrange
            var project = new List<TaskData>();
            var chart = new ApexChart<TaskData>();
            var teams = new List<ProjectTeam> { /* Populate with existing teams */ };
            var selectedTeamName = "TeamA";
            var newTask = new TaskData
            {
                Name = "New Task 123", // Task name with invalid characters
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(5),
                Comments = "Some comments..."
            };

            var taskManager = new TaskManager();

            // Act
            await taskManager.AddTask(project, chart, newTask, teams, selectedTeamName);

            // Assert
            var addedTask = project.FirstOrDefault(task => task.Name == "New Task 123"); // Replace with actual task name
            NUnit.Framework.Assert.IsNull(addedTask); // Assert that the task name should not comply with the defined criteria
                                      // Assert other conditions regarding task name validity if applicable
        }
        [Test]
        public async Task AddTask_ValidInput_StartDateBeforeEndDate()
        {
            // Arrange
            var project = new List<TaskData>();
            var chart = new ApexChart<TaskData>();
            var teams = new List<ProjectTeam> { /* Populate with existing teams */ };
            var selectedTeamName = "TeamA";
            var newTask = new TaskData
            {
                Name = "New Task",
                StartDate = DateTime.Now.AddDays(5), // Setting the start date after the end date
                EndDate = DateTime.Now.AddDays(3), // Setting the end date before the start date
                Comments = "Some comments..."
            };

            var taskManager = new TaskManager();

            // Act
            await taskManager.AddTask(project, chart, newTask, teams, selectedTeamName);

            // Assert
            var addedTask = project.FirstOrDefault(task => task.Name == "New Task"); // Replace with actual task name
            NUnit.Framework.Assert.IsNull(addedTask); // Assert that the task with incorrect date order is not added
                                      // Add other assertions if needed
        }
        [Test]
        public async Task AddTask_ValidInput_SelectedTeamExistsInProvidedTeams()
        {
            // Arrange
            var project = new List<TaskData>();
            var chart = new ApexChart<TaskData>();
            var teams = new List<ProjectTeam>
    {
        new ProjectTeam { Id = 1, Name = "TeamA" },
        new ProjectTeam { Id = 2, Name = "TeamB" },
        // Add other teams as needed for testing
    };
            var selectedTeamName = "TeamA";
            var newTask = new TaskData
            {
                Name = "New Task",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(5),
                Comments = "Some comments..."
            };

            var taskManager = new TaskManager();

            // Act
            taskManager.AddTask(project, chart, newTask, teams, selectedTeamName);

            // Assert
            var selectedTeam = teams.FirstOrDefault(team => team.Name == selectedTeamName);
            NUnit.Framework.Assert.IsNotNull(selectedTeam); // Assert that the selected team exists in the provided teams list
                                            // Add other assertions if needed
        }
    }
}
