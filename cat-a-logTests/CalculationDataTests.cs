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
                EndDate = DateTime.Now.AddDays(1) // Set any future date
                                                  // Other necessary properties can be initialized
            };

            var calculationData = new CalculationData(); // Instantiate the CalculationData class

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
                Progress = 50, // Assume progress is halfway or more towards completion
                AutoProgress = 50, // Assume auto-calculated progress is set
                EndDate = DateTime.Now.AddDays(5) // Set any future date
                                                  // Other necessary properties can be initialized
            };

            var calculationData = new CalculationData(); // Instantiate the CalculationData class

            // Act
            var result = calculationData.CheckEstimatedProgress(onScheduleTask);

            // Assert
            NUnit.Framework.Assert.AreEqual("Task On Schedule Task should be finished on time.", result);
        }
        [Test]
        public void CheckEstimatedProgress_TaskBehindSchedule_ReturnsTaskBehindScheduleMessage()
        {
            // Arrange
            var behindScheduleTask = new TaskData
            {
                Name = "Behind Schedule Task",
                Progress = 30, // Assume progress is less than the auto-calculated progress
                AutoProgress = 50, // Assume auto-calculated progress is set
                EndDate = DateTime.Now.AddDays(5) // Set any future date
                                                  // Other necessary properties can be initialized
            };

            var calculationData = new CalculationData(); // Instantiate the CalculationData class

            // Act
            var result = calculationData.CheckEstimatedProgress(behindScheduleTask);

            // Assert
            NUnit.Framework.Assert.AreEqual("Task Behind Schedule Task is behind schedule. Consider adding X more people.", result); // Replace X with the expected count
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
        public void CalculateAdditionalPeople_NoTeamMembers_ReturnsZero()
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
                Members = new List<string>() 
                                             
            };
            var calculationData = new CalculationData();
            // Act
            var result = calculationData.CalculateAdditionalPeople(task, team);

            // Assert
            NUnit.Framework.Assert.AreEqual(0, result);
        }
        [Test]
        public void CalculateAdditionalPeople_ProgressAtMinimum_ReturnsCorrectValue()
        {
            // Arrange
            var task = new TaskData
            {
                Name = "Task 1",
                Progress = 0, // Minimum value
                AutoProgress = 80 // Set any relevant values for your scenario
            };

            var team = new ProjectTeam
            {
                Name = "TeamA",
                Members = new List<string> { "John Doe", "Jane Doe" }
                // Other properties assignment
            };

            var calculationData = new CalculationData();
            int expectedValueForMinProgress = 5; // Define the expected value

            // Act
            var result = calculationData.CalculateAdditionalPeople(task, team);

            // Assert
            NUnit.Framework.Assert.AreEqual(expectedValueForMinProgress, result);
        }
        [Test]
        public void CalculateAdditionalPeople_NullTaskOrTeam_ReturnsZero()
        {
            // Arrange
            TaskData nullTask = null;
            ProjectTeam nullTeam = null;

            var calculationData = new CalculationData();

            // Act
            var resultNullTask = calculationData.CalculateAdditionalPeople(nullTask, new ProjectTeam());
            var resultNullTeam = calculationData.CalculateAdditionalPeople(new TaskData(), nullTeam);

            // Assert
            NUnit.Framework.Assert.AreEqual(0, resultNullTask);
            NUnit.Framework.Assert.AreEqual(0, resultNullTeam);
        }

        [Test]
        public void CalculateAdditionalPeople_TeamWithDuplicateMembers_ReturnsZero()
        {
            // Arrange
            var task = new TaskData
            {
                Name = "Task 1",
                Progress = 60,
                AutoProgress = 80
            };

            var teamWithDuplicates = new ProjectTeam
            {
                Name = "TeamA",
                Members = new List<string> { "John Doe", "Jane Doe", "John Doe" } // Duplicate member
            };

            var calculationData = new CalculationData();

            // Act
            var result = calculationData.CalculateAdditionalPeople(task, teamWithDuplicates);

            // Assert
            NUnit.Framework.Assert.AreEqual(0, result);
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
        public void CompareUserProgress_NullTaskData_ReturnsErrorMessage()
        {
            // Arrange
            TaskData task = null;
            var calculationData = new CalculationData();

            // Act
            var result = calculationData.CompareUserProgress(task);

            // Assert
            NUnit.Framework.Assert.AreEqual("Task data is null.", result);
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
        [Test]
        public void CalculateAutoProgress_NullTaskData_ReturnsZeroProgress()
        {
            // Arrange
            TaskData task = null;
            var calculationData = new CalculationData();

            // Act
            var result = calculationData.CalculateAutoProgress(task);

            // Assert
            NUnit.Framework.Assert.AreEqual(0, result);
        }









    }
}
