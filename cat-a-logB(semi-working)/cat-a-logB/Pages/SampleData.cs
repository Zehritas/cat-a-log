namespace cat_a_logB.Pages
{
    public class SampleData
    {
        private static List<GanttData> tasks = new List<GanttData>
        {
            new GanttData
            {
                Name = "Task 1",
                StartDate = DateTime.Now.Date.AddDays(-4),
                EndDate = DateTime.Now.Date.AddDays(1),
                Phase = GanttData.ProjectPhase.Planning,
                PointColor = GanttData.GetColorForPhase(GanttData.ProjectPhase.Planning)
            },
            new GanttData
            {
                Name = "Task 2",
                StartDate = DateTime.Now.Date.AddDays(-3),
                EndDate = DateTime.Now.Date.AddDays(2),
                Phase = GanttData.ProjectPhase.Planning,
                PointColor = GanttData.GetColorForPhase(GanttData.ProjectPhase.Planning)
            },
            new GanttData
            {
                Name = "Task 3",
                StartDate = DateTime.Now.Date.AddDays(-2),
                EndDate = DateTime.Now.Date.AddDays(5),
                Phase = GanttData.ProjectPhase.Execution,
                PointColor = GanttData.GetColorForPhase(GanttData.ProjectPhase.Execution)
            },
            new GanttData
            {
                Name = "Task 4",
                StartDate = DateTime.Now.Date.AddDays(-1),
                EndDate = DateTime.Now.Date.AddDays(8),
                Phase = GanttData.ProjectPhase.Execution,
                PointColor = GanttData.GetColorForPhase(GanttData.ProjectPhase.Execution)
            },
        };

        public static List<GanttData> GetProject()
        {
            return tasks;
        }
    }
}