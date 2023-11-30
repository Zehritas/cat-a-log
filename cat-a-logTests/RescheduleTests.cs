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


[TestFixture]
public class RescheduleTests
    {
    [Test]
    public void Valid_Rescheduling_FS_Dependency_SuccessorStartsAfterPredecessorEnds()
    {
        // Arrange
        var predecessorTask = new TaskData { Id = 1, StartDate = new DateTime(2023, 11, 1), EndDate = new DateTime(2023, 11, 12) };
        var successorTask = new TaskData { Id = 2, StartDate = new DateTime(2023, 11, 6), EndDate = new DateTime(2023, 11, 10) };
        var tasks = new List<TaskData> { predecessorTask, successorTask };

        var chartMock = new ApexChart<TaskData>();

        // Creating FS Dependency: Successor starts after predecessor ends
        var dependency = new Dependency { PredecessorTaskId = 1, SuccessorTaskId = 2, Type = DependencyType.FS };
        predecessorTask.Dependencies.Add(dependency);

        var taskManager = new TaskManager();

        // Act
        taskManager.Reschedule(predecessorTask, tasks, chartMock);

        // Assert
        var updatedSuccessorTask = tasks.FirstOrDefault(task => task.Id == 2);
        var expectedStartDate = predecessorTask.EndDate; // Expected start date of the successor task

        NUnit.Framework.Assert.IsNotNull(updatedSuccessorTask);
        NUnit.Framework.Assert.AreEqual(expectedStartDate, updatedSuccessorTask.StartDate);
    }
    [Test]
    public async Task Valid_Rescheduling_SF_Dependency_SuccessorStartsBeforePredecessorEnds()
    {
        // Arrange
        var predecessorTask = new TaskData
        {
            Id = 1,
            StartDate = new DateTime(2023, 11, 12),
            EndDate = new DateTime(2023, 11, 26),
            Dependencies = new List<Dependency>
        {
            new Dependency { SuccessorTaskId = 2, Type = DependencyType.SF }
        }
        };

        var successorTask = new TaskData
        {
            Id = 2,
            StartDate = new DateTime(2023, 11, 18),
            EndDate = new DateTime(2023, 11, 25)
        };

        var tasks = new List<TaskData> { predecessorTask, successorTask };
        var chart = new ApexChart<TaskData>();
        var taskManager = new TaskManager();

        // Act
        taskManager.Reschedule(predecessorTask, tasks, chart);

        // Assert
        var updatedSuccessorTask = tasks.FirstOrDefault(task => task.Id == 2);
        NUnit.Framework.Assert.AreEqual(new DateTime(2023, 11, 18), updatedSuccessorTask.StartDate);
        NUnit.Framework.Assert.AreEqual(new DateTime(2023, 11, 25), updatedSuccessorTask.EndDate);
    }
    [Test]
    public async Task Valid_Rescheduling_SS_Dependency_SuccessorStartsAfterPredecessorStarts()
    {
        // Arrange
        var predecessorTask = new TaskData
        {
            Id = 1,
            StartDate = new DateTime(2023, 11, 18),
            EndDate = new DateTime(2023, 11, 20),
            Dependencies = new List<Dependency>
        {
            new Dependency { SuccessorTaskId = 2, Type = DependencyType.SS }
        }
        };

        var successorTask = new TaskData
        {
            Id = 2,
            StartDate = new DateTime(2023, 11, 15),
            EndDate = new DateTime(2023, 11, 25)
        };

        var tasks = new List<TaskData> { predecessorTask, successorTask };
        var chart = new ApexChart<TaskData>();
        var taskManager = new TaskManager();

        // Act
        taskManager.Reschedule(predecessorTask, tasks, chart);

        // Assert
        var updatedSuccessorTask = tasks.FirstOrDefault(task => task.Id == 2);
        NUnit.Framework.Assert.AreEqual(new DateTime(2023, 11, 18), updatedSuccessorTask.StartDate);
        NUnit.Framework.Assert.AreEqual(new DateTime(2023, 11, 28), updatedSuccessorTask.EndDate);
    }
    [Test]
    public async Task Valid_Rescheduling_FF_Dependency_SuccessorFinishesAfterPredecessor()
    {
        // Arrange
        var predecessorTask = new TaskData
        {
            Id = 1,
            StartDate = new DateTime(2023, 11, 10),
            EndDate = new DateTime(2023, 11, 25),
            Dependencies = new List<Dependency>
        {
            new Dependency { SuccessorTaskId = 2, Type = DependencyType.FF }
        }
        };

        var successorTask = new TaskData
        {
            Id = 2,
            StartDate = new DateTime(2023, 11, 15),
            EndDate = new DateTime(2023, 11, 20)
        };

        var tasks = new List<TaskData> { predecessorTask, successorTask };
        var chart = new ApexChart<TaskData>();
        var taskManager = new TaskManager();

        // Act
        taskManager.Reschedule(predecessorTask, tasks, chart);

        // Assert
        var updatedSuccessorTask = tasks.FirstOrDefault(task => task.Id == 2);
        NUnit.Framework.Assert.AreEqual(new DateTime(2023, 11, 25), updatedSuccessorTask.EndDate);
    }
    [Test]
    public async Task InvalidDependency_TaskNotFound_ThrowsException()
    {
        // Arrange
        var tasks = new List<TaskData>
    {
        new TaskData { Id = 1, StartDate = DateTime.Today, EndDate = DateTime.Today.AddDays(1) }

    };

        var chart = new ApexChart<TaskData>();
        var taskManager = new TaskManager();

        // Act and Assert
        var exception = NUnit.Framework.Assert.ThrowsAsync<InvalidOperationException>(async () =>
        {
            await taskManager.Reschedule(new TaskData
            {
                Id = 1,
                Dependencies = new List<Dependency> { new Dependency { SuccessorTaskId = 2, Type = DependencyType.FS } }
            }, tasks, chart);
        });

        NUnit.Framework.Assert.AreEqual("Task not found for dependency: 2", exception.Message);
    }
    [Test]
    public async Task Reschedule_UnsupportedDependencyType_ThrowsArgumentException()
    {
        // Arrange
        var predecessorTask = new TaskData
        {
            Id = 1,
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(1),
            Dependencies = new List<Dependency>
        {
            new Dependency { SuccessorTaskId = 2, Type = DependencyType.Unknown }
        }
        };

        var successorTask = new TaskData
        {
            Id = 2
        };

        var tasks = new List<TaskData> { predecessorTask, successorTask };
        var chartMock = new ApexChart<TaskData>();
        var taskManager = new TaskManager();

        // Act 
        ArgumentException exception = null;
        try
        {
            await taskManager.Reschedule(predecessorTask, tasks, chartMock);
        }
        catch (ArgumentException ex)
        {
            exception = ex;
        }

        //Assert
        NUnit.Framework.Assert.IsNotNull(exception);
        NUnit.Framework.Assert.AreEqual("Unsupported DependencyType", exception.Message);
    }



}

