using System.ComponentModel.DataAnnotations;

namespace cat_a_logB.Data
{
    public class ProjectMilestone
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<TaskData> Tasks { get; set; }
        public string Color { get; set; }


        public ProjectMilestone(string name, List<TaskData> tasks, string color)
        {
            this.Name = name;
            this.Tasks = tasks;
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