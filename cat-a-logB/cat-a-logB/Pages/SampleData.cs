namespace cat_a_logB.Pages
{
    public class SampleData
    {
        public static List<GanttData> GetProjects()
        {
            var now = DateTime.Now;
            var projects = new List<GanttData>
            {
                new GanttData
                {
                    Name = "Task 1",
                    StartDate = DateTime.Now.Date.AddHours(-4), 
                    EndDate = DateTime.Now.AddHours(-1)
                },
                new GanttData
                {
                    Name = "Task 2",
                    StartDate = DateTime.Now.Date.AddHours(-3),
                    EndDate = DateTime.Now.Date.AddHours(2)
                },
                new GanttData
                {
                    Name = "Task 3",
                    StartDate = DateTime.Now.Date.AddHours(-2), 
                    EndDate = DateTime.Now.Date.AddHours(5)
                },
                new GanttData
                {
                    Name = "Task 4",
                    StartDate = DateTime.Now.Date.AddHours(-1), 
                    EndDate = DateTime.Now.AddHours(4)
                },
            };

            return projects;
        }
    }
}
