using AutoMapper;
using cat_a_logB.Data;
using cat_a_logB.Dto;
using cat_a_logB.Service.Interfaces;
using Newtonsoft.Json;
using System.Text;

namespace cat_a_logB.Service.Implementation
{
    public class UserService : IUserService
    {
        private readonly cat_a_logBContext _dbContext;
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;



        public UserService(cat_a_logBContext dbContext, IHttpClientFactory httpClientFactory, IMapper mapper)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public bool AddUser(User user) 
        {
            UserDto userDto = _mapper.Map<UserDto>(user);
            string data = JsonConvert.SerializeObject(userDto);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = _httpClient.PostAsync(_httpClient.BaseAddress + "/User", content).Result;

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public bool AddUsers(List<User> users) 
        {
            _dbContext.User.AddRange(users);
            return Save();
        }

        public bool RemoveUser(int id)
        {
            HttpResponseMessage response = _httpClient.DeleteAsync(_httpClient.BaseAddress + "/User/" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public bool RemoveUsers(List<User> users)
        {
            _dbContext.User.RemoveRange(users);
            return Save();
        }

        public User? GetUser(int id)
        {
            User? user = null;
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/User/" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                user = JsonConvert.DeserializeObject<User>(data);
            }

            return user;
        }

        public List<User>? GetUsers()
        {
            List<User>? users = null;
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/User").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                users = JsonConvert.DeserializeObject<List<User>>(data);
            }

            return users;
        }

        public bool Save()
        {
            var saved = _dbContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateUser(User user)
        {
            UserDto userDto = _mapper.Map<UserDto>(user);
            string data = JsonConvert.SerializeObject(userDto);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = _httpClient.PutAsync(_httpClient.BaseAddress + "/User", content).Result;

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public bool UserExists(int id)
        {
            return _dbContext.User.Any(u => u.Id == id);
        }
    }
}
