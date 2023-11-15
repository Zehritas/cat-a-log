namespace cat_a_logB.Data
{
    public class ProjectMilestone
    {
        public string Name { get; set; }
        public List<GanttData> Tasks { get; set; }
        public string Color { get; set; }


        public ProjectMilestone(string name, List<GanttData> tasks, string color)
        {
            this.Name = name;
            this.Tasks = tasks;
            this.Color = color;
        }
        public ProjectMilestone(string name)
        {
            Name = name;
            Tasks = new List<GanttData>();

        }

        public ProjectMilestone()
        {
        }

        public void LoadMilestoneTasks(List<GanttData> allTasks)
        {
            Tasks = allTasks.Where(task => task.Name == Name).ToList();
        }

        public void CalculateCompletedTasksPercentage()
        {
            if (Tasks.Count == 0)
            {
                return;
            }

            int completedTasks = Tasks.Count(task => task.Progress == 100);
            double completedTasksPercentage = (double)completedTasks / Tasks.Count * 100;

            if (completedTasksPercentage == 100)
            {
                Color = "green";
            }

            return;
        }




    }
}