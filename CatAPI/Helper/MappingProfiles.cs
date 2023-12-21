using AutoMapper;
using CatAPI.Data;
using CatAPI.Dto;

namespace CatAPI.Helper
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
