namespace cat_a_logB.Data
{
    public class ProjectMilestone
    {
        public string Name { get; set; }
        public List<GanttData> Tasks { get; set; }
        public string Color { get; set; }

        public DateTime TargetDate { get; set; }


        public ProjectMilestone(string name, List<GanttData> tasks, DateTime targetDate, string color)
        {
            this.Name = name;
            this.Tasks = tasks;
            this.TargetDate = targetDate;
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


        public enum TaskCompletionStatus
        {
            Incomplete,
            Completed
        }


        public TaskCompletionStatus GetTaskCompletionStatus()
        {
            if (Tasks.Count == 0)
            {
                return TaskCompletionStatus.Incomplete;
            }

            int completedTasks = Tasks.Count(task => task.Progress == 100);

            if (completedTasks == Tasks.Count)
            {
                return TaskCompletionStatus.Completed;
            }

            return TaskCompletionStatus.Incomplete;
        }





    }
}