using AutoMapper;
using cat_a_logB.Data;
using cat_a_logB.Dto;
using cat_a_logB.Service.Interfaces;
using Newtonsoft.Json;
using System.Text;

namespace cat_a_logB.Service.Implementation
{
    public class MilestoneService : IMilestoneService
    {
        private readonly cat_a_logBContext _dbContext;
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;


        public MilestoneService(cat_a_logBContext dbContext, IHttpClientFactory httpClientFactory, IMapper mapper)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public bool AddMilestone(ProjectMilestone milestone)
        {
            var milestoneDto = _mapper.Map<MilestoneDto>(milestone);
            string data = JsonConvert.SerializeObject(milestoneDto);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = _httpClient.PostAsync(_httpClient.BaseAddress + "/Milestone", content).Result;

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public bool RemoveMilestone(int id)
        {
            HttpResponseMessage response = _httpClient.DeleteAsync(_httpClient.BaseAddress + "/Milestone/" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public bool AddMilestones(List<ProjectMilestone> projectMilestones)
        {
            foreach (ProjectMilestone projectMilestone in projectMilestones)
            {
                _dbContext.ProjectMilestone.Add(projectMilestone);
            }
            return Save();
        }

        public bool RemoveMilestones(List<ProjectMilestone> projectMilestones)
        {
            foreach (ProjectMilestone projectMilestone in projectMilestones)
            {
                _dbContext.ProjectMilestone.Remove(projectMilestone);
            }
            return Save();
        }

        public ProjectMilestone? GetMilestone(int Id)
        {
            ProjectMilestone? milestone = null;
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/Milestone/" + Id).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                milestone = JsonConvert.DeserializeObject<ProjectMilestone>(data);
            }

            return milestone;
        }

        public List<ProjectMilestone>? GetMilestones()
        {
            List<ProjectMilestone>? milestones = null;
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/Milestone").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                milestones = JsonConvert.DeserializeObject<List<ProjectMilestone>>(data);
            }

            return milestones;
        }

        public void ChangeMilestoneColor(int id, string color)
        {
            ProjectMilestone milestone = _dbContext.ProjectMilestone.Find(id);
            milestone.Color = color;
            _dbContext.SaveChanges();
        }

        public bool Save()
        {
            var saved = _dbContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateMilestone(ProjectMilestone milestone)
        {
            var milestoneDto = _mapper.Map<ProjectMilestone>(milestone);
            string data = JsonConvert.SerializeObject(milestoneDto);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = _httpClient.PutAsync(_httpClient.BaseAddress + "/Milestone", content).Result;

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public bool MilestoneExists(int id)
        {
            return _dbContext.ProjectMilestone.Any(m => m.Id == id);
        }
    }
}
