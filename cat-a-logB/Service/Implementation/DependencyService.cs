using AutoMapper;
using cat_a_logB.Data;
using cat_a_logB.Dto;
using cat_a_logB.Service.Interfaces;
using Newtonsoft.Json;
using System.Text;

namespace cat_a_logB.Service.Implementation
{
    public class DependencyService : IDependencyService
    {
        private readonly cat_a_logBContext _dbContext;
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;

        public DependencyService(cat_a_logBContext dbContext, IHttpClientFactory httpClientFactory, IMapper mapper)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public bool AddDependency(Dependency dependency)
        {
            DependencyDto dependencyDto = _mapper.Map<DependencyDto>(dependency);
            string data = JsonConvert.SerializeObject(dependencyDto);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = _httpClient.PostAsync(_httpClient.BaseAddress + "/Dependency", content).Result;

            if(response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public bool RemoveDependency(int id)
        {
            HttpResponseMessage response = _httpClient.DeleteAsync(_httpClient.BaseAddress + "/Dependency/" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public bool AddDependencies(List<Dependency> dependencies)
        {
            foreach (Dependency dependency in dependencies)
            {
                _dbContext.Dependency.Add(dependency);
            }
            return Save();
        }

        public bool RemoveDependencies(List<Dependency> dependencies, int taskId)
        {
            foreach (Dependency dependency in dependencies)
            {
                if (taskId == dependency.SuccessorTaskId)
                {
                    RemoveDependency(dependency.Id);
                }
            }
            return Save();
        }

        public Dependency? GetDependency(int Id) 
        {
            Dependency? dependency = null;
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/Dependency/" + Id).Result;

            if(response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                dependency = JsonConvert.DeserializeObject<Dependency>(data);
            }

            return dependency;
        }

        public List<Dependency>? GetDependencies()
        {
            List<Dependency>? dependencies = null;
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/Dependency").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                dependencies = JsonConvert.DeserializeObject<List<Dependency>>(data);
            }

            return dependencies;
        }

        public bool Save()
        {
            var saved = _dbContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateDependency(Dependency dependency)
        {
            DependencyDto dependencyDto = _mapper.Map<DependencyDto>(dependency);
            string data = JsonConvert.SerializeObject(dependencyDto);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = _httpClient.PutAsync(_httpClient.BaseAddress + "/Dependency", content).Result;

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public bool DependencyExists(int id)
        {
            return _dbContext.Dependency.Any(d => d.Id == id);
        }
    }
}
