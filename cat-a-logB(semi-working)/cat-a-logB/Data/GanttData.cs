public class GanttData
{
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string PointColor { get; set; }

    public enum ProjectPhase // enum reqioement, might change to const strings
    {
        Planning,
        Execution,
        Testing,
        Deployment,
        // Add more phases as needed
    }

    public ProjectPhase Phase { get; set; }

    public GanttData()
    {
        StartDate = DateTime.Now.Date;
        EndDate = DateTime.Now.Date;
    }

    public static string GetColorForPhase(ProjectPhase phase)
    {
        switch (phase)
        {
            case ProjectPhase.Planning:
                return "#3db821";

            case ProjectPhase.Execution:
                return "#b8212b";

            case ProjectPhase.Testing:
                return "#2188b8";

            case ProjectPhase.Deployment:
                return "#2188b8";

            default:
                return "#000000";
        }
    }

}
