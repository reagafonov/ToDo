using AutoMapper;
using ToDo.WebApi.Domain.Entities;
using ToDo.WebApi.Repos.FiltersData;
using ToDo.WebApi.Repos.UserTaskLists;

namespace ToDo.WebApi.ServiceDomain;

public class MapperProfile:Profile
{
    public MapperProfile()
    {
        CreateMap<UserTaskDto, UserTask>()
            .ForMember(userTask => userTask.TypeUserTaskList, expression => expression.Ignore())
            .ForMember(userTask => userTask.Created, expression => expression.MapFrom(dto=>dto.Created.ToUniversalTime()))
            .ForMember(userTask => userTask.CompleteDate, 
                expression => expression.MapFrom(dto=>dto.CompleteDate.HasValue ? dto.CompleteDate.Value.ToUniversalTime(): (DateTime?)null))
            .ReverseMap();

        CreateMap<UserTaskFilterDto, UserTaskFilterData>()
            .ForMember(userTaskFilter => userTaskFilter.Id, expression => expression.Ignore())
            .ReverseMap();

        CreateMap<UserTaskListDto, UserTaskList>()
            .ForMember(member=>member.UserTasks, expression => expression.Ignore())
            .ReverseMap();
        
        CreateMap<UserTaskListFilterDto, UserTaskListFilter>();
    }
}