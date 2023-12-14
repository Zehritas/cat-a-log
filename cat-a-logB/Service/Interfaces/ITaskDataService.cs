using cat_a_logB.Data;

namespace cat_a_logB.Service.Interfaces
{
    public interface ITaskDataService
    {
        public bool AddTask(TaskData task);

        public bool RemoveTask(int id);

        public bool AddTasks(List<TaskData> tasks);

        public bool RemoveTasks(List<TaskData> tasks);

        public List<TaskData> GetTasks();

        public TaskData GetTask(int id);

        public void SyncColorWithTeam(TaskData task);

        public string GetTaskName(int Id);

        public void ChangeTaskComment(int taskId, string newComment);

        public void ChangeTaskStartDate(int taskId, DateTime newStartDate);

        public void ChangeTaskEndDate(int taskId, DateTime newEndDate);

        public void ChangeTaskName(int taskId, string newName);

        public bool UpdateTask(TaskData task);

        public bool TaskExists(int id);
    }
}
