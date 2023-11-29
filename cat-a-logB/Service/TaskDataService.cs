using cat_a_logB.Data;
using cat_a_logB.Pages;
using System.Linq;

namespace cat_a_logB.Service
{
    public class TaskDataService : ITaskDataService
    {
         private readonly cat_a_logBContext _dbContext;

         public TaskDataService(cat_a_logBContext dbContext)
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
            task = _dbContext.TaskData.Where(t => t.Id == Id).FirstOrDefault();

            return task.Name;
        }
    }
}
