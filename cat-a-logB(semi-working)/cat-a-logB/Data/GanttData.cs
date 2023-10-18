public class GanttData
{
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string PointColor { get; set; }
    public ProjectTeam Team { get; set; }
    public int Progress { get; set; } // Add a Progress property


    public GanttData()
    {
        StartDate = DateTime.Now.Date;
        EndDate = DateTime.Now.Date;
        PointColor = "#000000";
        Progress = 0;
    }
    public GanttData(string name, DateTime startDate, DateTime endDate, ProjectTeam team, int progress)
    {
        Name = name;
        StartDate = startDate;
        EndDate = endDate;
        Team = team;
        Progress = progress;
        PointColor = team.Color; // Assuming 'color' is a property of the ProjectTeam class
    }


    // public static string GetTeamColor(ProjectTeam Team)
    // {
    //     switch (phase)
    //     {
    //         case ProjectPhase.Planning:
    //             return "#3db821";

    //         case ProjectPhase.Execution:
    //             return "#b8212b";

    //         case ProjectPhase.Testing:
    //             return "#2188b8";

    //         case ProjectPhase.Deployment:
    //             return "#2188b8";

    //         default:
    //             return "#000000";
    //     }
    // }
}