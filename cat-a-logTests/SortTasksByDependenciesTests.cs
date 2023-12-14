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
    public class SortTasksByDependenciesTests
    {
        [Test]
        public void SortTasksByDependencies_EmptyList_ReturnsEmptyList()
        {
            var taskManager = new TaskManager();
            var emptyTasks = new List<TaskData>();

            var sortedTasks = taskManager.SortTasksByDependencies<TaskData, Dependency>(emptyTasks);

            NUnit.Framework.Assert.AreEqual(0, sortedTasks.Count);
        }

        [Test]
        public void SortTasksByDependencies_TasksWithoutDependencies_ReturnsSameOrder()
        {

            var tasks = new List<TaskData>
            {
                new TaskData { Id = 1, Name = "Task 1", Dependencies = new List<Dependency>() },
                new TaskData { Id = 2, Name = "Task 2", Dependencies = new List<Dependency>() },
                new TaskData { Id = 3, Name = "Task 3", Dependencies = new List<Dependency>() }
        
            };

            var taskManager = new TaskManager();

            var sortedTasks = taskManager.SortTasksByDependencies<TaskData, Dependency>(tasks);

            
            for (var i = 0; i < tasks.Count; i++)
            {
                NUnit.Framework.Assert.AreEqual(tasks[i].Id, sortedTasks[i].Id);
                NUnit.Framework.Assert.AreEqual(tasks[i].Name, sortedTasks[i].Name);
            }
        }
        [Test]
        public void SortTasksByDependencies_LinearDependencies_ReturnsCorrectOrder()
        {
            var tasks = new List<TaskData>
                {
                    new TaskData { Id = 1, Name = "Task 1", Dependencies = new List<Dependency>() },
                    new TaskData { Id = 2, Name = "Task 2", Dependencies = new List<Dependency> { new Dependency { SuccessorTaskId = 1 } } },
                     new TaskData { Id = 3, Name = "Task 3", Dependencies = new List<Dependency> { new Dependency { SuccessorTaskId = 2 } } }

                };

            var taskManager = new TaskManager();

            var sortedTasks = taskManager.SortTasksByDependencies<TaskData, Dependency>(tasks);

       
            NUnit.Framework.Assert.AreEqual(1, sortedTasks[0].Id);
            NUnit.Framework.Assert.AreEqual(2, sortedTasks[1].Id);
            NUnit.Framework.Assert.AreEqual(3, sortedTasks[2].Id);
         
        }
        //    [Test]
        //    public void SortTasksByDependencies_CyclicDependencies_ReturnsCorrectOrder()
        //    {
        //        var tasks = new List<TaskData>
        //{
        //    new TaskData { Id = 1, Name = "Task 1", Dependencies = new List<Dependency> { new Dependency { SuccessorTaskId = 2 } } },
        //    new TaskData { Id = 2, Name = "Task 2", Dependencies = new List<Dependency> { new Dependency { SuccessorTaskId = 3 } } },
        //    new TaskData { Id = 3, Name = "Task 3", Dependencies = new List<Dependency> { new Dependency { SuccessorTaskId = 1 } } }

        //};

        //        var taskManager = new TaskManager();

        //        var sortedTasks = taskManager.SortTasksByDependencies<TaskData, Dependency>(tasks);

        //        NUnit.Framework.Assert.AreEqual(3, sortedTasks[0].Id);
        //        NUnit.Framework.Assert.AreEqual(2, sortedTasks[1].Id);
        //        NUnit.Framework.Assert.AreEqual(1, sortedTasks[2].Id);


        //    }

        [Test]
        public void SortTasksByDependencies_InvalidDependencies_ReturnsValidTasks()
        {
            var tasks = new List<TaskData>
            {
                new TaskData { Id = 1, Name = "Task 1", Dependencies = new List<Dependency> { new Dependency { SuccessorTaskId = 2 } } },
                new TaskData { Id = 2, Name = "Task 2", Dependencies = new List<Dependency> { new Dependency { SuccessorTaskId = 3 } } },
                new TaskData { Id = 3, Name = "Task 3" },
        
           };

            var taskManager = new TaskManager();

            var sortedTasks = taskManager.SortTasksByDependencies<TaskData, Dependency>(tasks);

            
            NUnit.Framework.Assert.AreEqual(tasks.Count, sortedTasks.Count);

        }
        [Test]
        public void SortTasksByDependencies_CheckSortingOrder()
        {
            var tasks = new List<TaskData>
            {
                new TaskData { Id = 1, Name = "Task 1", Dependencies = new List<Dependency> { new Dependency { SuccessorTaskId = 2 } } },
                new TaskData { Id = 2, Name = "Task 2", Dependencies = new List<Dependency> { new Dependency { SuccessorTaskId = 3 } } },
                new TaskData { Id = 3, Name = "Task 3" },

            };

            var taskManager = new TaskManager();

            var sortedTasks = taskManager.SortTasksByDependencies<TaskData, Dependency>(tasks);

            NUnit.Framework.Assert.AreEqual("Task 3", sortedTasks[0].Name);
            NUnit.Framework.Assert.AreEqual("Task 2", sortedTasks[1].Name);
            NUnit.Framework.Assert.AreEqual("Task 1", sortedTasks[2].Name);

        }



    }
}
