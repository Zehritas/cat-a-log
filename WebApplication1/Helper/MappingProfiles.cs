using AutoMapper;
using Cat_a_logAPI.Data;
using Cat_a_logAPI.Dto;

namespace Cat_a_logAPI.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Dependency, DependencyDto>();
            CreateMap<DependencyDto, Dependency>();
            CreateMap<Member, MemberDto>();
            CreateMap<MemberDto, Member>();
            CreateMap<Project, ProjectDto>();
            CreateMap<Project, ProjectDto>();
            CreateMap<ProjectDto, Project>();
            CreateMap<ProjectMilestone, MilestoneDto>();
            CreateMap<MilestoneDto, ProjectMilestone>();
            CreateMap<ProjectTeam, TeamDto>();
            CreateMap<TeamDto, ProjectTeam>();
            CreateMap<TaskData, TaskDto>();
            CreateMap<TaskDto, TaskData>();
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }
    }
}
