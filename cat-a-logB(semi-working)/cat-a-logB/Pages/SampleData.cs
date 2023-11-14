using cat_a_logB.Data;

namespace cat_a_logB.Pages
{
    public class SampleData
    {
        private static List<ProjectTeam> teams = new List<ProjectTeam>
        {
            new ProjectTeam("#3db821", "Team A", new List<string> {"Marijonas", "Rytis"}),
            new ProjectTeam("#b8212b", "Team B", new List<string> {"Jūris", "Aldas"}),
            new ProjectTeam("#2188b8", "Team C", new List<string> {"Aldas", "Rytis", "Jūris", "Marijonas"})
        };

        private static List<GanttData> tasks = new List<GanttData>
        {
            new GanttData(
                "Task 1",
                DateTime.Now.Date.AddDays(-4),
                DateTime.Now.Date.AddDays(1),
                teams[0], // Assigning Task 1 to Team A
                0,
                ""
            ),
            new GanttData(
                "Task 2",
                DateTime.Now.Date.AddDays(-3),
                DateTime.Now.Date.AddDays(2),
                teams[0], // Assigning Task 1 to Team A
                0,
                ""
            ),
            new GanttData(
                "Task 3",
                DateTime.Now.Date.AddDays(-2),
                DateTime.Now.Date.AddDays(5),
                teams[1], // Assigning Task 1 to Team A
                0,
                ""
            ),
            new GanttData(
                "Task 4",
                DateTime.Now.Date.AddDays(-1),
                DateTime.Now.Date.AddDays(8),
                teams[1], // Assigning Task 1 to Team A
                0,
                ""
            ),
            new GanttData(
                "Task 5",
                DateTime.Now.Date.AddDays(-2),
                DateTime.Now.Date.AddDays(10),
                teams[2], // Assigning Task 1 to Team A
                0,
                ""
            )
        };

        public static List<GanttData> GetProject()
        {

            foreach (var task in tasks)
            {
                task.AutoProgress = CalculateAutoProgress(task);
            }
            return tasks;
        }
        public static List<ProjectMilestone> GetMilestones()
        {
            return milestones;
        }
        private static List<ProjectMilestone> milestones = new List<ProjectMilestone>
        {
            new ProjectMilestone { Name = "Milestone 1", Tasks = new List<GanttData>() {tasks[1],tasks[2] } },
            new ProjectMilestone { Name = "Milestone 2", Tasks = new List<GanttData>() {tasks[0],tasks[4] }},

        };
        public static List<ProjectTeam> GetTeams()
        {
            return teams;
        }
        private static int CalculateAutoProgress(GanttData task)
        {

            DateTime currentDate = DateTime.Now.Date;
            if (currentDate < task.StartDate)
            {
                return 0;
            }
            else if (currentDate >= task.EndDate)
            {
                return 100;
            }
            else
            {

                double totalDays = (task.EndDate - task.StartDate).TotalDays;
                double daysPassed = (currentDate - task.StartDate).TotalDays;
                return (int)((daysPassed / totalDays) * 100);
            }
        }
    }
}

