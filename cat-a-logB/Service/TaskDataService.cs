using cat_a_logB.Data;

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

         public void RemoveTask(TaskData task)
         {
             _dbContext.TaskData.Remove(task);
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
    }
}
