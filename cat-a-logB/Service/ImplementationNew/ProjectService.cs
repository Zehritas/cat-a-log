using AutoMapper;
using cat_a_logB.Data;
using cat_a_logB.Dto;
using cat_a_logB.Service.InterfacesNew;
using Microsoft.DotNet.Scaffolding.Shared.ProjectModel;
using Newtonsoft.Json;
using System.Text;

namespace cat_a_logB.Service.ImplementationNew
{
    public class ProjectService : IProjectService
    {
        private readonly cat_a_logBContext _dbContext;
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;


        public ProjectService(cat_a_logBContext dbContext, IHttpClientFactory httpClientFactory, IMapper mapper)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public bool AddProject(Project project)
        {
            var projectDto = _mapper.Map<ProjectDto>(project);
            string data = JsonConvert.SerializeObject(projectDto);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = _httpClient.PostAsync(_httpClient.BaseAddress + "/Project", content).Result;

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public bool AddProjects(IEnumerable<Project> projects)
        {
            _dbContext.Project.AddRange(projects);
            return Save();
        }

        public Project? GetProject(int Id)
        {
            Project? project = null;
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/Project/" + Id).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                project = JsonConvert.DeserializeObject<Project>(data);
            }

            return project;
        }

        public IEnumerable<Project>? GetProjects()
        {
            List<Project>? projects = null;
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/Project").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                projects = JsonConvert.DeserializeObject<List<Project>>(data);
            }

            return projects;
        }

        public bool ProjectExists(int id)
        {
            return _dbContext.Project.Any(p => p.Id == id);
        }

        public bool RemoveProject(int id)
        {
            HttpResponseMessage response = _httpClient.DeleteAsync(_httpClient.BaseAddress + "/Projet/" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public bool RemoveProjects(IEnumerable<Project> projects)
        {
            _dbContext.Project.RemoveRange(projects);
            return Save();
        }

        public bool Save()
        {
            var saved = _dbContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateProject(Project project)
        {
            var projectDto = _mapper.Map<Project>(project);
            string data = JsonConvert.SerializeObject(projectDto);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = _httpClient.PutAsync(_httpClient.BaseAddress + "/Project", content).Result;

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
    }
}
