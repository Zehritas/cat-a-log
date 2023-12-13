using cat_a_logB.Data;
using cat_a_logB.Service.Interfaces;

namespace cat_a_logB.Service.Implementation
{
    public class MemberService : IMemberService
    {
        private readonly cat_a_logBContext _dbContext;

        public MemberService(cat_a_logBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool AddMember(Member member)
        {
            _dbContext.Member.Add(member);
            return Save();
        }

        public bool AddMembers(IEnumerable<Member> members)
        {
            _dbContext.Member.AddRange(members);
            return Save();
        }

        public Member GetMember(int userId, int teamId)
        {
            return _dbContext.Member.Where(m => m.UserId == userId && m.TeamId == teamId).FirstOrDefault();
        }

        public IEnumerable<Member> GetMembers()
        {
            return _dbContext.Member.ToList();
        }

        public bool MemberExists(int userId, int teamId)
        {
            return _dbContext.Member.Any(m => m.UserId == userId && m.TeamId == teamId);
        }

        public bool RemoveMember(Member member)
        {
            _dbContext.Member.Remove(member);
            return Save();
        }

        public bool RemoveMembers(IEnumerable<Member> members)
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
            _dbContext.Member.Update(member);

            return Save();
        }
    }
}