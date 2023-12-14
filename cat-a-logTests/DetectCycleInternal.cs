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
        public async Task DetectCycle_NoDependencies_ReturnsFalse()
        {
            // Arrange
            var task = new TaskData { Id = 1, Dependencies = new List<Dependency>() };
            var taskManager = new TaskManager();

            // Act
            var result = await taskManager.DetectCycleInternal(task, task, new HashSet<TaskData>(), new HashSet<TaskData>());

            // Assert
            NUnit.Framework.Assert.IsFalse(result);
        }
        

    }
}
