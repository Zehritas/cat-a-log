﻿using CatAPI.Data;

namespace CatAPI.Service.Interfaces
{
    public interface IMemberService
    {
        public bool AddMember(Member member);

        public bool AddMembers(IEnumerable<Member> members);

        public bool RemoveMember(int userId, int teamId);

        public bool RemoveMembers(IEnumerable<Member> members);

        public Member GetMember(int userId, int teamId);

        public IEnumerable<Member> GetMembers();

        public bool UpdateMember(Member member);

        public bool MemberExists(int userId, int teamId);
    }
}