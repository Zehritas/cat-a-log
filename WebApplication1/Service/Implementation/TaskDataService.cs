using Cat_a_logAPI.Data;
using Cat_a_logAPI.Service.Interfaces;

namespace Cat_a_logAPI.Service.Implementation
{
    public class TaskDataService : ITaskDataService
    {
        private readonly Cat_a_logBContext _dbContext;

        public TaskDataService(Cat_a_logBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddTask(TaskData task)
        {
            _dbContext.TaskData.Add(task);
            _dbContext.SaveChanges();
        }

        public void RemoveTask(TaskData taskToRemove)
        {
            List<Dependency> dependenciesToRemove;
            List<ProjectTeam> allTeams = _dbContext.ProjectTeam.ToList();
            foreach (ProjectTeam team in allTeams)
            {
                foreach (TaskData task in team.Tasks)
                {
                    dependenciesToRemove = _dbContext.Dependency.Where(d => d.SuccessorTaskId == taskToRemove.Id).ToList();
                    _dbContext.RemoveRange(dependenciesToRemove);
                }
            }

            _dbContext.TaskData.Remove(taskToRemove);
            _dbContext.SaveChanges();
        }

        public void AddTasks(List<TaskData> tasks)
        {
            foreach (TaskData task in tasks)
            {
                _dbContext.TaskData.Add(task);
            }
            _dbContext.SaveChanges();
        }

        public void RemoveTasks(List<TaskData> tasks)
        {
            foreach (TaskData task in tasks)
            {
                _dbContext.TaskData.Remove(task);
            }
            _dbContext.SaveChanges();
        }

        public List<TaskData> GetAllTasks()
        {
            return _dbContext.TaskData.ToList();
        }

        public void SyncColorWithTeam(TaskData task)
        {
            ProjectTeam projectTeam = _dbContext.ProjectTeam.Where(t => task.TeamId == t.Id).FirstOrDefault();
            task.PointColor = projectTeam.Color;
            _dbContext.SaveChanges();
        }

        public string GetTaskName(int Id)
        {
            TaskData task;
            task = _dbContext.TaskData.Find(Id);

            return task.Name;
        }

        public void ChangeTaskComment(int taskId, string newComment)
        {
            TaskData task = _dbContext.TaskData.Find(taskId);
            task.Comments = newComment;
            _dbContext.SaveChanges();
        }

        public void ChangeTaskStartDate(int taskId, DateTime newStartDate)
        {
            TaskData task = _dbContext.TaskData.Find(taskId);
            task.StartDate = newStartDate;
            _dbContext.SaveChanges();
        }

        public void ChangeTaskEndDate(int taskId, DateTime newEndDate)
        {
            TaskData task = _dbContext.TaskData.Find(taskId);
            task.EndDate = newEndDate;
            _dbContext.SaveChanges();
        }

        public void ChangeTaskName(int taskId, string newName)
        {
            TaskData task = _dbContext.TaskData.Find(taskId);
            task.Name = newName;
            _dbContext.SaveChanges();
        }

        public TaskData UpdateTask(TaskData task)
        {
            TaskData updatedTask = _dbContext.TaskData.Find(task.Id);

            return updatedTask;
        }

    }
}
