using AutoMapper;
using cat_a_logB.Data;
using cat_a_logB.Dto;
using cat_a_logB.Service.Interfaces;
using Newtonsoft.Json;
using System.Text;

namespace cat_a_logB.Service.Implementation
{
    public class MemberService : IMemberService
    {
        private readonly cat_a_logBContext _dbContext;
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;



        public MemberService(cat_a_logBContext dbContext, IHttpClientFactory httpClientFactory, IMapper mapper)
        {
            _dbContext = dbContext;
            _httpClient = httpClientFactory.CreateClient("ApiClient");
            _mapper = mapper;
        }

        public bool AddMember(Member member)
        {
            var memberDto = _mapper.Map<MemberDto>(member);
            string data = JsonConvert.SerializeObject(memberDto);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = _httpClient.PostAsync(_httpClient.BaseAddress + "/Member", content).Result;

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public bool AddMembers(List<Member> members)
        {
            _dbContext.Member.AddRange(members);
            return Save();
        }

        public Member? GetMember(int userId, int teamId)
        {
            Member? member = null;
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/Member/" + userId + "/" + teamId).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                member = JsonConvert.DeserializeObject<Member>(data);
            }

            return member;
        }

        public List<Member>? GetMembers()
        {
            List<Member>? members = null;
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/Member").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                members = JsonConvert.DeserializeObject<List<Member>>(data);
            }

            return members;
        }

        public bool MemberExists(int userId, int teamId)
        {
            return _dbContext.Member.Any(m => m.UserId == userId && m.TeamId == teamId);
        }

        public bool RemoveMember(int userId, int teamId)
        {
            HttpResponseMessage response = _httpClient.DeleteAsync(_httpClient.BaseAddress + "/Member/" + userId + "/" + teamId).Result;

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public bool RemoveMembers(List<Member> members)
        {
            _dbContext.Member.RemoveRange(members);
            return Save();
        }

        public bool Save()
        {
            var saved = _dbContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateMember(Member member)
        {
            var memberDto = _mapper.Map<MemberDto>(member);
            string data = JsonConvert.SerializeObject(memberDto);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = _httpClient.PutAsync(_httpClient.BaseAddress + "/Member", content).Result;

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
    }
}