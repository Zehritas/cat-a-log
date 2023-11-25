using cat_a_logB.Data;

namespace cat_a_logB.Service
{
    public interface ITaskDataService
    {
        public void AddTask(TaskData task);

        public void RemoveTask(TaskData task);

        public void AddTasks(List<TaskData> tasks);

        public void RemoveTasks(List<TaskData> tasks);
    }
}
