namespace cat_a_logB.Pages
{
    public class SampleData
    {
        private static List<GanttData> projects = new List<GanttData>
        {
            new GanttData
            {
                Name = "Task 1",
                StartDate = DateTime.Now.Date.AddDays(-4),
                EndDate = DateTime.Now.AddDays(-1)
            },
            new GanttData
            {
                Name = "Task 2",
                StartDate = DateTime.Now.Date.AddDays(-3),
                EndDate = DateTime.Now.Date.AddHours(2)
            },
            new GanttData
            {
                Name = "Task 3",
                StartDate = DateTime.Now.Date.AddDays(-2),
                EndDate = DateTime.Now.Date.AddDays(5)
            },
        };

        public static List<GanttData> GetProjects()
        {
            return projects;
        }

        public static void AddProject(GanttData project)
        {
            projects.Add(project);
        }
    }
}