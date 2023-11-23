using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cat_a_logB.Data;
using cat_a_logB.Pages;
using NuGet.Protocol.Plugins;
using NUnit.Framework;
using static cat_a_logB.Data.ProjectMilestone;


[TestFixture]
public class ProjectMilestoneTest
{

    [Test]
    public void GetTaskCompletionStatus_NoTasks_ReturnsIncomplete()
    {
        // Arrange
        var milestone = new ProjectMilestone("Milestone without tasks");

        // Act
        var result = milestone.GetTaskCompletionStatus();

        // Assert
        NUnit.Framework.Assert.AreEqual(ProjectMilestone.TaskCompletionStatus.Incomplete, result);
    }

    [Test]
    public void GetTaskCompletionStatus_AllTasksCompleted_ReturnsCompleted()
    {
        // Arrange
        var tasks = new List<GanttData>
            {
                new GanttData { Progress = 100 },
                new GanttData { Progress = 100 },
                // Add more completed tasks if needed for testing
            };
        var milestone = new ProjectMilestone("Milestone with completed tasks", tasks, DateTime.Now, "Green");

        // Act
        var result = milestone.GetTaskCompletionStatus();

        // Assert
        NUnit.Framework.Assert.AreEqual(ProjectMilestone.TaskCompletionStatus.Completed, result);
    }
    [Test]
    public void GetTaskCompletionStatus_MixedTasks_ReturnsIncomplete()
    {
        // Arrange
        var tasks = new List<GanttData>
            {
                new GanttData { Progress = 100 },
                new GanttData { Progress = 50 },

            };
        var milestone = new ProjectMilestone("Milestone with mixed tasks", tasks, DateTime.Now, "Yellow");

        // Act
        var result = milestone.GetTaskCompletionStatus();

        // Assert
        NUnit.Framework.Assert.AreEqual(ProjectMilestone.TaskCompletionStatus.Incomplete, result);
    }
}