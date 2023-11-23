using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cat_a_logB.Data;
using cat_a_logB.Pages;
using Microsoft.AspNetCore.Components;
using NUnit.Framework;

[TestFixture]
public class GanttDataTests
    {
    [Test]
    public void GanttData_DefaultConstructor_ShouldSetDefaultValues()
    {
        // Arrange & Act
        var ganttData = new GanttData();

        // Assert
        NUnit.Framework.Assert.AreEqual(DateTime.Now.Date, ganttData.StartDate);
        NUnit.Framework.Assert.AreEqual(DateTime.Now.Date, ganttData.EndDate);
        NUnit.Framework.Assert.AreEqual("#000000", ganttData.PointColor);
        NUnit.Framework.Assert.AreEqual(0, ganttData.Progress);
        NUnit.Framework.Assert.AreEqual("", ganttData.Comments);
        NUnit.Framework.Assert.IsNull(ganttData.Team); 
    }

    [Test]
    public void GanttData_ParameterizedConstructor_ShouldSetValuesCorrectly()
    {
        // Arrange
        var team = new ProjectTeam("Red", "Team A");
        var startDate = new DateTime(2023, 11, 1);
        var endDate = new DateTime(2023, 11, 10);
        const int progress = 50;
        const string comments = "Some comments";

        // Act
        var ganttData = new GanttData("Task 1", startDate, endDate, team, progress, comments);

        // Assert
        NUnit.Framework.Assert.AreEqual("Task 1", ganttData.Name);
        NUnit.Framework.Assert.AreEqual(startDate, ganttData.StartDate);
        NUnit.Framework.Assert.AreEqual(endDate, ganttData.EndDate);
        NUnit.Framework.Assert.AreEqual("Red", ganttData.Team.Color); 
        NUnit.Framework.Assert.AreEqual(progress, ganttData.Progress);
        NUnit.Framework.Assert.AreEqual(comments, ganttData.Comments);
        NUnit.Framework.Assert.AreEqual(team, ganttData.Team);
    }
    [Test]
    public void CompareTo_ShouldReturnZero_WhenThisProgressEqualsOtherProgress()
    {

        var ganttData1 = new GanttData { Progress = 30 };
        var ganttData2 = new GanttData { Progress = 30 };

      
        var result = ganttData1.CompareTo(ganttData2);


        NUnit.Framework.Assert.AreEqual(0, result);
    }
}

