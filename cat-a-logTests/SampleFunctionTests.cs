using ApexCharts;
using cat_a_logB.Data;
using cat_a_logB.Pages;
using Microsoft.AspNetCore.Components;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[TestFixture]
public class SampleFunctionsTest
{

    [Test]
    public void CalculateAutoProgress_ShouldReturnZero_WhenCurrentDateIsBeforeStartDate()
    {

        var task = new TaskData
        {
            StartDate = DateTime.Now.Date.AddDays(5),
            EndDate = DateTime.Now.Date.AddDays(10)
        };


        int result = SampleData.CalculateAutoProgress(task);


        NUnit.Framework.Assert.AreEqual(0, result);
    }

    [Test]
    public void CalculateAutoProgress_ShouldReturnHundred_WhenCurrentDateIsAfterEndDate()
    {
        // Arrange
        var task = new TaskData
        {
            StartDate = DateTime.Now.Date.AddDays(-10),
            EndDate = DateTime.Now.Date.AddDays(-5)
        };

        // Act
        int result = SampleData.CalculateAutoProgress(task);

        // Assert
        NUnit.Framework.Assert.AreEqual(100, result);
    }
    [Test]
    public void CalculateAutoProgress_ShouldReturnCalculatedPercentage_WhenWithinStartAndEndDate()
    {
        // Arrange
        var task = new TaskData
        {
            StartDate = DateTime.Now.Date.AddDays(-10),
            EndDate = DateTime.Now.Date.AddDays(10)
        };

        // Act
        int result = SampleData.CalculateAutoProgress(task);


        double totalDays = (task.EndDate - task.StartDate).TotalDays;
        double daysPassed = (DateTime.Now.Date - task.StartDate).TotalDays;
        int expectedValue = (int)((daysPassed / totalDays) * 100);


        NUnit.Framework.Assert.AreEqual(expectedValue, result);
    }

}