using AutoMapper;
using ToDo.WebApi.Domain.Entities;
using ToDo.WebApi.Repos.UserTaskLists;
using ToDo.WebApi.Repos.UserTasks;

namespace ToDo.WebApi.ServiceDomain;

public class MapperProfile:Profile
{
    public MapperProfile()
    {
        CreateMap<UserTaskDto, UserTask>()
            .ForMember(userTask => userTask.TypeUserTaskList, expression => expression.Ignore())
            .ReverseMap();

        CreateMap<UserTaskFilterDto, UserTaskFilterData>()
            .ForMember(userTaskFilter => userTaskFilter.Id, expression => expression.Ignore())
            .ReverseMap();

        CreateMap<UserTaskListDto, UserTaskList>()
            .ForMember(member=>member.UserTasks, expression => expression.Ignore())
            .ReverseMap();
        
        CreateMap<UserTaskListFilterDto, UserTaskListFilter>();

        CreateMap<UserDto, User>()
            .ForMember(user => user.Id, expression => expression.MapFrom(y => Guid.NewGuid()));
    }
}