using ApexCharts;
using AutoMapper;
using cat_a_logB.Data;
using cat_a_logB.Dto;
using cat_a_logB.Service.Interfaces;
using Newtonsoft.Json;
using System.Text;

namespace cat_a_logB.Service.Implementation
{
    public class ProjectTeamService : IProjectTeamService
    {
        private readonly cat_a_logBContext _dbContext;
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;



        public ProjectTeamService(cat_a_logBContext dbContext, IHttpClientFactory httpClientFactory, IMapper mapper)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public bool AddTeam(ProjectTeam projectTeam)
        {
            var teamDto = _mapper.Map<TeamDto>(projectTeam);
            string data = JsonConvert.SerializeObject(teamDto);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = _httpClient.PostAsync(_httpClient.BaseAddress + "/Team", content).Result;

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public bool RemoveTask(TaskData taskToRemove)
        {
            List<Data.Dependency> dependenciesToRemove;
            List<ProjectTeam> allTeams = _dbContext.ProjectTeam.ToList();
            foreach (ProjectTeam team in allTeams)
            {
                foreach (TaskData task in _dbContext.TaskData.Where(t => t.TeamId == team.Id).ToList()) 
                {
                    dependenciesToRemove = _dbContext.Dependency.Where(d => d.SuccessorTaskId == taskToRemove.Id).ToList();
                    _dbContext.RemoveRange(dependenciesToRemove);
                }
            }

            _dbContext.TaskData.Remove(taskToRemove);
            return Save();
        }

        public bool RemoveTeam(int id)
        {
            HttpResponseMessage response = _httpClient.DeleteAsync(_httpClient.BaseAddress + "/Team/" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public bool AddTeams(List<ProjectTeam> projectTeams)
        {
            foreach (ProjectTeam projectTeam in projectTeams)
            {
                _dbContext.ProjectTeam.Add(projectTeam);
            }
            return Save();
        }

        public bool RemoveTeams(List<ProjectTeam> projectTeams)
        {
            foreach (ProjectTeam projectTeam in projectTeams)
            {
                _dbContext.ProjectTeam.Remove(projectTeam);
            }
            return Save();
        }

        public ProjectTeam? GetTeam(int Id)
        {
            ProjectTeam? team = null;
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/Team/" + Id).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                team = JsonConvert.DeserializeObject<ProjectTeam>(data);
            }

            return team;
        }

        public List<ProjectTeam>? GetTeams()
        {
            List<ProjectTeam>? teams = null;
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/Team").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                teams = JsonConvert.DeserializeObject<List<ProjectTeam>>(data);
            }

            return teams;
        }

        public bool Save()
        {
            var saved = _dbContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateTeam(ProjectTeam team)
        {
            var teamDto = _mapper.Map<TeamDto>(team);
            string data = JsonConvert.SerializeObject(teamDto);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = _httpClient.PutAsync(_httpClient.BaseAddress + "/Team", content).Result;

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public bool TeamExists(int id)
        {
            return _dbContext.ProjectTeam.Any(t => t.Id == id);
        }

        public List<TaskData> GetTasksForTeam(int id)
        {
            var teamTasks = _dbContext.TaskData.Where(t => t.TeamId == id).ToList();

            return teamTasks;
        }

        public List<Member> GetTeamMembers(int id)
        {
            var teamMembers = _dbContext.Member.Where(m => m.TeamId == id).ToList();

            return teamMembers;
        }


    }
}
