﻿public class GanttData : IComparable<GanttData>
{
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string PointColor { get; set; }
    public string Comments { get; set; }
    public ProjectTeam Team { get; set; }
    public int Progress { get; set; } // Add a Progress property

    public double DayProgress { get; set; }

    public int AutoProgress { get; set; }
    public List<Dependency> Dependencies { get; set; } = new List<Dependency>();
    public int CompareTo(GanttData other)
    {
        return this.Progress.CompareTo(other.Progress);
    }


    public GanttData()
    {
        StartDate = DateTime.Now.Date;
        EndDate = DateTime.Now.Date;
        PointColor = "#000000";
        Progress = 0;
        Comments = "";
    }
    public GanttData(string name, DateTime startDate, DateTime endDate, ProjectTeam team, int progress, string comments)
    {
        Name = name;
        StartDate = startDate;
        EndDate = endDate;
        Team = team;
        Progress = progress;
        PointColor = team.Color; // Assuming 'color' is a property of the ProjectTeam class
        Comments = comments;
    }

}