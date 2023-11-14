namespace cat_a_logB.Data
{
    public class ProjectMilestone
    {
        public string Name { get; set; }
        public List<GanttData> Tasks { get; set; }

        public event Action OnTasksUpdated;

        public void UpdateTasks(List<GanttData> updatedTasks)
        {
            Tasks = updatedTasks;
            OnTasksUpdated?.Invoke();
            UpdateDotColors();
        }

        private void UpdateDotColors()
        {
            // Add your logic to update dot colors here
            // For example, iterate through tasks and update the colors based on completion status
            foreach (var task in Tasks)
            {
                if (task.Progress == 100)
                {
                    task.PointColor = "green";
                }
                else
                {
                    task.PointColor = task.Team.Color;
                }
            }

            // Trigger a re-render of the component
            OnTasksUpdated?.Invoke();
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

        public double CompletedTasksPercentage
        {
            get
            {
                if (Tasks.Count == 0)
                    return 0;

                int completedTasks = Tasks.Count(task => task.Progress == 100);
                return (double)completedTasks / Tasks.Count * 100;
            }

        }



    }
}