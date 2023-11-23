using cat_a_logB.Data;

namespace cat_a_logB.Pages
{
    public class SampleData
    {
        private static List<ProjectTeam> teams = new List<ProjectTeam>
        {
            new ProjectTeam("#3db821", "Team A", new List<String> {"Marijonas", "Rytis"}),
            new ProjectTeam("#b8212b", "Team B", new List<String> {"Jūris", "Aldas"}),
            new ProjectTeam("#2188b8", "Team C", new List<String> {"Aldas", "Rytis", "Jūris", "Marijonas"})
        };

        private static List<Data.TaskData> tasks = new List<Data.TaskData>
        {
            new Data.TaskData(
                "Task 1",
                DateTime.Now.Date.AddDays(-4),
                DateTime.Now.Date.AddDays(1),
                teams[0], // Assigning Task 1 to Team A
                0,
                ""
            ),
            new Data.TaskData(
                "Task 2",
                DateTime.Now.Date.AddDays(-3),
                DateTime.Now.Date.AddDays(2),
                teams[0], // Assigning Task 1 to Team A
                0,
                ""
            ),
            new Data.TaskData(
                "Task 3",
                DateTime.Now.Date.AddDays(-2),
                DateTime.Now.Date.AddDays(5),
                teams[1], // Assigning Task 1 to Team A
                0,
                ""
            ),
            new Data.TaskData(
                "Task 4",
                DateTime.Now.Date.AddDays(-1),
                DateTime.Now.Date.AddDays(8),
                teams[1], // Assigning Task 1 to Team A
                0,
                ""
            ),
            new Data.TaskData(
                "Task 5",
                DateTime.Now.Date.AddDays(-2),
                DateTime.Now.Date.AddDays(10),
                teams[2], // Assigning Task 1 to Team A
                0,
                ""
            )
        };

        public static List<Data.TaskData> GetProject()
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

            new ProjectMilestone { Name = "Milestone 1", Tasks = new List<Data.TaskData>() { tasks[1], tasks[2] ,} , TargetDate = new DateTime(2023, 11, 26),   Color = "blue"},
            new ProjectMilestone { Name = "Milestone 2", Tasks = new List<Data.TaskData>() { tasks[0], tasks[4] ,} , TargetDate = new DateTime(2023, 11, 22), Color= "blue"},

        };
        public static List<ProjectTeam> GetTeams()
        {
            return teams;
        }

        public static int CalculateAutoProgress(TaskData task)
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

