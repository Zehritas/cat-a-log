using System.ComponentModel.DataAnnotations;

namespace cat_a_logB.Data
{
    public class ProjectMilestone
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<TaskData> Tasks { get; set; }
        public string? Color { get; set; }

        public DateTime TargetDate { get; set; } = DateTime.Now;



        public ProjectMilestone(string name, List<TaskData> tasks, DateTime targetDate, string color)
        {
            this.Name = name;
            this.Tasks = tasks;
            this.TargetDate = targetDate;
            this.Color = color;
        }
        public ProjectMilestone(string name)
        {
            Name = name;
            Tasks = new List<TaskData>();

        }

        public ProjectMilestone()
        {
        }

        public void LoadMilestoneTasks(List<TaskData> allTasks)
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