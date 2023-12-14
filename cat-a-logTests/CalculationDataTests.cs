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
    public class CalculationDataTests
    {
        [Test]
        public void CheckEstimatedProgress_TaskComplete_ReturnsTaskCompleteMessage()
        {
            // Arrange
            var completedTask = new TaskData
            {
                Name = "Completed Task",
                Progress = 100,
                EndDate = DateTime.Now.AddDays(1) 
                                                  
            };

            var calculationData = new CalculationData(); 

            // Act
            var result = calculationData.CheckEstimatedProgress(completedTask);

            // Assert
            NUnit.Framework.Assert.AreEqual("Task Completed Task is already complete.", result);
        }

        [Test]
        public void CheckEstimatedProgress_TaskOnSchedule_ReturnsTaskOnScheduleMessage()
        {
            // Arrange
            var onScheduleTask = new TaskData
            {
                Name = "On Schedule Task",
                Progress = 50, 
                AutoProgress = 50, 
                EndDate = DateTime.Now.AddDays(5) 
                                                  
            };

            var calculationData = new CalculationData(); 

            // Act
            var result = calculationData.CheckEstimatedProgress(onScheduleTask);

            // Assert
            NUnit.Framework.Assert.AreEqual("Task On Schedule Task should be finished on time.", result);
        }
        
        [Test]
        public void CalculateAdditionalPeople_ValidInput_ReturnsCorrectValue()
        {
            // Arrange
            var task = new TaskData
            {
                Name = "Task 1",
                Progress = 60,
                AutoProgress = 80 
                                 
            };

            var team = new ProjectTeam
            {
                Name = "TeamA",
                Members = new List<string> { "John Doe", "Jane Doe" }
                
            };

          
            var calculationData = new CalculationData();
            // Act
            var result = calculationData.CalculateAdditionalPeople(task, team);

            // Assert
            // Assertions for expected additional people needed
            const int expectedValue = 4;
            NUnit.Framework.Assert.AreEqual(expectedValue, result);
        }
       

       

        [Test]
        public void CompareUserProgress_TaskCompleted_ReturnsTaskCompletedMessage()
        {
            // Arrange
            var completedTask = new TaskData
            {
                Name = "Completed Task",
                Progress = 100,
                StartDate = DateTime.Now.AddDays(-5), // Set any relevant values for your scenario
                EndDate = DateTime.Now.AddDays(5)
                // Other necessary properties can be initialized
            };

            var calculationData = new CalculationData();

            // Act
            var result = calculationData.CompareUserProgress(completedTask);

            // Assert
            NUnit.Framework.Assert.AreEqual("Task completed.", result);
        }
        [Test]
        public void CompareUserProgress_UserBehindSchedule_ReturnsDaysBehindMessage()
        {
            // Arrange
            var task = new TaskData
            {
                Name = "Task 1",
                Progress = 60, // User's progress
                AutoProgress = 80, // Expected progress
                StartDate = DateTime.Now.AddDays(-10), // Adjust based on your scenario
                EndDate = DateTime.Now.AddDays(10)
                // Other necessary properties can be initialized
            };

            var calculationData = new CalculationData();

            // Act
            var result = calculationData.CompareUserProgress(task);

            // Assert
            NUnit.Framework.Assert.AreEqual("User is 4.0 days behind.", result); // Adjust expected message based on your scenario
        }
        [Test]
        public void CompareUserProgress_UserAheadOfSchedule_ReturnsDaysAheadMessage()
        {
            // Arrange
            var task = new TaskData
            {
                Name = "Task 1",
                Progress = 90, // User's progress
                AutoProgress = 80, // Expected progress
                StartDate = DateTime.Now.AddDays(-10), // Adjust based on your scenario
                EndDate = DateTime.Now.AddDays(10)
                // Other necessary properties can be initialized
            };

            var calculationData = new CalculationData();

            // Act
            var result = calculationData.CompareUserProgress(task);

            // Assert
            NUnit.Framework.Assert.AreEqual("User is 2.0 days ahead.", result); // Adjust expected message based on your scenario
        }
        [Test]
        public void CompareUserProgress_UserOnTrack_ReturnsOnTrackMessage()
        {
            // Arrange
            var task = new TaskData
            {
                Name = "Task 1",
                Progress = 80, // User's progress
                AutoProgress = 80, // Expected progress
                StartDate = DateTime.Now.AddDays(-10), // Adjust based on your scenario
                EndDate = DateTime.Now.AddDays(10)
                // Other necessary properties can be initialized
            };

            var calculationData = new CalculationData();

            // Act
            var result = calculationData.CompareUserProgress(task);

            // Assert
            NUnit.Framework.Assert.AreEqual("User is on track.", result);
        }
    

        [Test]
        public void CalculateAutoProgress_CurrentDateWithinTaskDuration_ReturnsValidProgress()
        {
            // Arrange
            var startDate = DateTime.Today.AddDays(-5); // Set any relevant start date
            var endDate = DateTime.Today.AddDays(10); // Set any relevant end date
            

            var task = new TaskData
            {
                StartDate = startDate,
                EndDate = endDate,
                // Other necessary properties initialization
            };

            var calculationData = new CalculationData();

            // Act
            var result = calculationData.CalculateAutoProgress(task);

            // Assert
            NUnit.Framework.Assert.GreaterOrEqual(result, 0);
            NUnit.Framework.Assert.LessOrEqual(result, 100);
        }
        [Test]
        public void CalculateAutoProgress_CurrentDateAfterTaskDuration_ReturnsCompletedProgress()
        {
            // Arrange
            var startDate = DateTime.Today.AddDays(-10); // Set any relevant start date
            var endDate = DateTime.Today.AddDays(-2); // Set any relevant end date
            var currentDate = DateTime.Today.AddDays(5); // Set a current date after the task duration

            var task = new TaskData
            {
                StartDate = startDate,
                EndDate = endDate,
                // Other necessary properties initialization
            };

            var calculationData = new CalculationData();

            // Act
            var result = calculationData.CalculateAutoProgress(task);

            // Assert
            NUnit.Framework.Assert.AreEqual(100, result);
        }
        [Test]
        public void CalculateAutoProgress_CurrentDateBeforeTaskStarts_ReturnsZeroProgress()
        {
            // Arrange
            var startDate = DateTime.Today.AddDays(5); // Set any relevant start date
            var endDate = DateTime.Today.AddDays(15); // Set any relevant end date
            

            var task = new TaskData
            {
                StartDate = startDate,
                EndDate = endDate,
                // Other necessary properties initialization
            };

            var calculationData = new CalculationData();

            // Act
            var result = calculationData.CalculateAutoProgress(task);

            // Assert
            NUnit.Framework.Assert.AreEqual(0, result);
        }
        









    }
}
