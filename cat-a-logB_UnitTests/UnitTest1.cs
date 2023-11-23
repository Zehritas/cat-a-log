using cat_a_logB;

using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

[TestFixture]
public class ProjectTeamTest
{
    
    [Test]
    public void LoadTeamTasks_ShouldFilterTasksForTeam()
    {
        
        var allTasks = new List<GanttData>
        {
            new GanttData { Team = new ProjectTeam("Red", "Team A") },
            new GanttData { Team = new ProjectTeam("Blue", "Team B") },
            new GanttData { Team = new ProjectTeam("Red", "Team A") }
        };

        var team = new ProjectTeam("Red", "Team A");

        
        team.LoadTeamTasks(allTasks);

        
        NUnit.Framework.Assert.AreEqual(2, team.Tasks.Count);
        NUnit.Framework.Assert.IsTrue(team.Tasks.All(t => t.Team.Name == team.Name));
    }

    [Test]
    public void GetTasksForTeam_ShouldFilterTasksForGivenTeam()
    {
        
        var allTasks = new List<GanttData>
        {
            new GanttData { Team = new ProjectTeam("Red", "Team A") },
            new GanttData { Team = new ProjectTeam("Blue", "Team B") },
            new GanttData { Team = new ProjectTeam("Red", "Team A") }
        };

        var team = new ProjectTeam("Red", "Team A");

        
        ProjectTeam.GetTasksForTeam(allTasks, team);

        
        NUnit.Framework.Assert.AreEqual(2, team.Tasks.Count);
        NUnit.Framework.Assert.IsTrue(team.Tasks.All(t => t.Team.Name == team.Name));
    }

    [Test]
    public void MembersSetter_NullInput_ShouldThrowArgumentException()
    {
        
        var team = new ProjectTeam();

        NUnit.Framework.Assert.Throws<ArgumentException>(() => team.Members = null);
    }

}
