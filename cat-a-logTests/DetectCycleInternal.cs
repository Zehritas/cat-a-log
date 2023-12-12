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
    public class DetectCycleInternal
    {
        [Test]
        public async Task DetectCycleInternal_NoCycles_ReturnsFalse()
        {
            // Arrange
            var task1 = new TaskData { Id = 1, Name = "Task 1", Dependencies = new List<Dependency>() };
            var task2 = new TaskData { Id = 2, Name = "Task 2", Dependencies = new List<Dependency>() };
            var task3 = new TaskData { Id = 3, Name = "Task 3", Dependencies = new List<Dependency>() };

            task1.Dependencies.Add(new Dependency { SuccessorTaskId = 2 });
            task2.Dependencies.Add(new Dependency { SuccessorTaskId = 3 });
            task3.Dependencies.Add(new Dependency { SuccessorTaskId = 1 }); // Task 3 depends on Task 1 to create a cycle

            var project = new List<TaskData> { task1, task2, task3 };

            var taskManager = new TaskManager();

            var visited = new HashSet<TaskData>();
            var currentlyVisiting = new HashSet<TaskData>();

            // Act
            var hasCycle = await taskManager.DetectCycleInternal(task1, task1, visited, currentlyVisiting);

            // Assert
            NUnit.Framework.Assert.IsTrue(hasCycle); // Expecting a cycle
        }

    }
}
