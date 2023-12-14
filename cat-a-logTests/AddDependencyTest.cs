using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using ApexCharts;
using cat_a_logB.Data;
using cat_a_logB.Pages;
using cat_a_logB.Service;
using Microsoft.AspNetCore.Components;
using NuGet.Protocol.Plugins;
using NUnit.Framework;

namespace cat_a_logTests
{
    [TestFixture]
    public class AddDependencyTest
    {
        [Test]
        public async Task AddDependency_ValidInput_SuccessfullyAddsDependency()
        {
            // Arrange
            var project = new List<TaskData>
            {
                new TaskData { Id = 1, Name = "Task 1", Dependencies = new List<Dependency>() },
                new TaskData { Id = 2, Name = "Task 2", Dependencies = new List<Dependency>() }
             };

            var chart = new ApexChart<TaskData>();
            var selectedData = new SelectedData<TaskData>
            {
                DataPoint = new DataPoint<TaskData>
                {
                    Items = new List<TaskData>
            {
                new TaskData { Id = 1, Dependencies = new List<Dependency>() }
            }
                }
            };

            var selectedDependencyType = DependencyType.FS;
            var selectedSuccessorTask = 2;

            var taskManager = new TaskManager();

            // Act
            taskManager.AddDependency(project, chart, selectedData, selectedDependencyType, selectedSuccessorTask);

            // Assert
            var predecessorTask = project.FirstOrDefault(task => task.Id == 1);
            NUnit.Framework.Assert.IsNotNull(predecessorTask);
            NUnit.Framework.Assert.IsTrue(predecessorTask.Dependencies.Any(dep => dep.SuccessorTaskId == selectedSuccessorTask));
        }
        [Test]
        public async Task AddDependency_InvalidInput_NoDependencyAdded()
        {
            // Arrange
            var project = new List<TaskData>
    {
        new TaskData { Id = 1, Name = "Task 1", Dependencies = new List<Dependency>() },
        new TaskData { Id = 2, Name = "Task 2", Dependencies = new List<Dependency>() }
    };

            var chart = new ApexChart<TaskData>();
            var selectedData = new SelectedData<TaskData>
            {
                DataPoint = new DataPoint<TaskData>
                {
                    Items = new List<TaskData>
            {
                new TaskData { Id = 1, Dependencies = new List<Dependency>() }
            }
                }
            };

            var selectedDependencyType = DependencyType.FS;
            var selectedSuccessorTask = 0; // Invalid selectedSuccessorTaskId

            var taskManager = new TaskManager();

            // Act
            taskManager.AddDependency(project, chart, selectedData, selectedDependencyType, selectedSuccessorTask);

            // Assert
            var predecessorTask = project.FirstOrDefault(task => task.Id == 1);
            NUnit.Framework.Assert.IsNotNull(predecessorTask);

            // Ensure that no new dependency has been added because of the invalid input
            NUnit.Framework.Assert.IsFalse(predecessorTask.Dependencies.Any());
        }

        [Test]
        public async Task AddDependency_AlreadyExists_NoDuplicateDependencyAdded()
        {
            // Arrange
            var project = new List<TaskData>
    {
        new TaskData { Id = 1, Name = "Task 1", Dependencies = new List<Dependency> { new Dependency { SuccessorTaskId = 2 } } },
        new TaskData { Id = 2, Name = "Task 2", Dependencies = new List<Dependency>() }
    };

            var chart = new ApexChart<TaskData>();
            var selectedData = new SelectedData<TaskData>
            {
                DataPoint = new DataPoint<TaskData>
                {
                    Items = new List<TaskData>
            {
                new TaskData { Id = 1, Dependencies = new List<Dependency>() }
            }
                }
            };

            var selectedDependencyType = DependencyType.FS;
            var selectedSuccessorTask = 2; // Trying to add an existing dependency

            var taskManager = new TaskManager();

            // Act
            taskManager.AddDependency(project, chart, selectedData, selectedDependencyType, selectedSuccessorTask);

            // Assert
            var predecessorTask = project.FirstOrDefault(task => task.Id == 1);
            NUnit.Framework.Assert.IsNotNull(predecessorTask);

            // Ensure that no duplicate dependency is added
            NUnit.Framework.Assert.AreEqual(1, predecessorTask.Dependencies.Count(dep => dep.SuccessorTaskId == selectedSuccessorTask));
        }
        [Test]
        public async Task AddDependency_SuccessfulUpdate_DataCorrectlyUpdated()
        {
            // Arrange
            var project = new List<TaskData>
            {
                new TaskData { Id = 1, Name = "Task 1", Dependencies = new List<Dependency>() },
                new TaskData { Id = 2, Name = "Task 2", Dependencies = new List<Dependency>() }
            };

            var chart = new ApexChart<TaskData>();
            var selectedData = new SelectedData<TaskData>
            {
                DataPoint = new DataPoint<TaskData>
                {
                    Items = new List<TaskData>
            {
                new TaskData { Id = 1, Dependencies = new List<Dependency>() }
            }
                }
            };

            var selectedDependencyType = DependencyType.FS;
            var selectedSuccessorTask = 2;

            var taskManager = new TaskManager();

            // Act
            taskManager.AddDependency(project, chart, selectedData, selectedDependencyType, selectedSuccessorTask);

            // Assert
            var predecessorTask = project.FirstOrDefault(task => task.Id == 1);
            NUnit.Framework.Assert.IsNotNull(predecessorTask);
            NUnit.Framework.Assert.IsTrue(predecessorTask.Dependencies.Any(dep => dep.SuccessorTaskId == selectedSuccessorTask));

        }
    }
}
