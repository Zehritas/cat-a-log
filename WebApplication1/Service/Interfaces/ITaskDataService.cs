using Cat_a_logAPI.Data;

namespace Cat_a_logAPI.Service.Interfaces
{
    public interface ITaskDataService
    {
        public void AddTask(TaskData task);

        public void RemoveTask(TaskData task);

        public void AddTasks(List<TaskData> tasks);

        public void RemoveTasks(List<TaskData> tasks);

        public List<TaskData> GetAllTasks();

        public void SyncColorWithTeam(TaskData task);

        public string GetTaskName(int Id);

        public void ChangeTaskComment(int taskId, string newComment);

        public void ChangeTaskStartDate(int taskId, DateTime newStartDate);

        public void ChangeTaskEndDate(int taskId, DateTime newEndDate);

        public void ChangeTaskName(int taskId, string newName);

        public TaskData UpdateTask(TaskData task);
    }
}
