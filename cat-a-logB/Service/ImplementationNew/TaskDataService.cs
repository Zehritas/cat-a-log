using AutoMapper;
using cat_a_logB.Data;
using cat_a_logB.Dto;
using cat_a_logB.Service.ImplementationNew ;
using cat_a_logB.Service.InterfacesNew;
using Microsoft.Build.Evaluation;
using Microsoft.DotNet.Scaffolding.Shared.ProjectModel;
using Newtonsoft.Json;
using System.Text;

namespace cat_a_logB.Service.ImplementationNew
{
    public class TaskDataService : ITaskDataService
    {
        private readonly cat_a_logBContext _dbContext;
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;



        public TaskDataService(cat_a_logBContext dbContext, IHttpClientFactory httpClientFactory, IMapper mapper)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public bool AddTask(TaskData task)
        {
            var taskDto = _mapper.Map<TaskDto>(task);
            string data = JsonConvert.SerializeObject(taskDto);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = _httpClient.PostAsync(_httpClient.BaseAddress + "/TaskData", content).Result;

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public bool RemoveTask(int id)
        {
            HttpResponseMessage response = _httpClient.DeleteAsync(_httpClient.BaseAddress + "/TaskData/" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
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
                _dbContext.TaskData.Remove(task);
            }
            return Save();
        }

        public IEnumerable<TaskData>? GetTasks()
        {
            List<TaskData>? tasks = null;
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/TaskData").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                tasks = JsonConvert.DeserializeObject<List<TaskData>>(data);
            }

            return tasks;
        }

        public TaskData? GetTask(int Id)
        {
            TaskData? task = null;
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/TaskData/" + Id).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                task = JsonConvert.DeserializeObject<TaskData>(data);
            }

            return task;
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
            var taskDto = _mapper.Map<TaskDto>(task);
            string data = JsonConvert.SerializeObject(taskDto);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = _httpClient.PutAsync(_httpClient.BaseAddress + "/TaskData", content).Result;

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public bool TaskExists(int id)
        {
            return _dbContext.TaskData.Any(t => t.Id == id);
        }
    }
}
