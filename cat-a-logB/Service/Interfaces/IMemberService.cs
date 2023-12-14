using cat_a_logB.Data;

namespace cat_a_logB.Service.Interfaces
{
    public interface IMemberService
    {
        public bool AddMember(Member member);

        public bool AddMembers(List<Member> members);

        public bool RemoveMember(int userId, int teamId);

        public bool RemoveMembers(List<Member> members);

        public Member GetMember(int userId, int teamId);

        public List<Member> GetMembers();

        public bool UpdateMember(Member member);

        public bool MemberExists(int userId, int teamId);
    }
}