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

        public bool AddTask(TaskData task)
        {
            _dbContext.TaskData.Add(task);
            return Save();
        }

        public bool RemoveTask(TaskData taskToRemove)
        {
            IEnumerable<Dependency> dependenciesToRemove;
            IEnumerable<ProjectTeam> allTeams = _dbContext.ProjectTeam.ToList();
            foreach (ProjectTeam team in allTeams)
            {
                foreach (TaskData task in team.Tasks)
                {
                    dependenciesToRemove = _dbContext.Dependency.Where(d => d.SuccessorTaskId == taskToRemove.Id).ToList();
                    _dbContext.RemoveRange(dependenciesToRemove);
                }
            }

            var milestone = _dbContext.ProjectMilestone.Find(taskToRemove.MilestoneId);

            _dbContext.TaskData.Remove(taskToRemove);

            if (milestone != null)
            {
                if(!_dbContext.TaskData.Any(t => t.MilestoneId == milestone.Id))
                {
                    _dbContext.ProjectMilestone.Remove(milestone);
                }
            }

            return Save();
        }

        public bool AddTasks(IEnumerable<TaskData> tasks)
        {
            foreach (TaskData task in tasks)
            {
                _dbContext.TaskData.Add(task);
            }
            return Save();
        }

        public bool RemoveTasks(IEnumerable <TaskData> tasks)
        {
            foreach (TaskData task in tasks)
            {
                RemoveTask(task);
            }
            return Save();
        }

        public IEnumerable<TaskData> GetTasks()
        {
            return _dbContext.TaskData.ToList();
        }

        public TaskData GetTask(int Id)
        {
            return _dbContext.TaskData.Find(Id);
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

        public bool Save()
        {
            var saved = _dbContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateTask(TaskData task)
        {
            _dbContext.TaskData.Update(task);

            return Save();
        }

        public bool TaskExists(int id)
        {
            return _dbContext.TaskData.Any(t => t.Id == id);
        }
    }
}
