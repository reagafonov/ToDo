using AutoMapper;
using MongoDB.Bson;
using ToDo.WebApi.Domain.Entities;
using ToDo.WebApi.Repos.FiltersData;
using ToDo.WebApi.Repos.UserTaskLists;

namespace ToDo.WebApi.ServiceDomain;

public class MapperProfile:Profile
{
    /// Название поля для даты создания
    /// </summary>
    private const string UploadDateFieldName = "uploadDate";

    private const string NameFieldName = "fileName";
    private const string UserTaskIdFieldName = "userTaskId";
    private const string IdFieldName = "_id";
    private const string DataFieldName = "data";
    
    public MapperProfile()
    {
        CreateMap<UserTaskDto, UserTask>()
            .ForMember(userTask => userTask.TypeUserTaskList, expression => expression.Ignore())
            .ForMember(userTask => userTask.Created, expression => expression.MapFrom(dto=>dto.Created.ToUniversalTime()))
            .ForMember(userTask => userTask.CompleteDate, 
                expression => expression.MapFrom(dto=>dto.CompleteDate.HasValue ? dto.CompleteDate.Value.ToUniversalTime(): (DateTime?)null))
            .ReverseMap();

        CreateMap<UserTaskUpdateDto, UserTask>()
            .ForMember(userTask => userTask.TypeUserTaskList, expression => expression.Ignore())
            .ForMember(userTask => userTask.Created,
                expression => expression.Ignore())
            .ForMember(userTask => userTask.CompleteDate,
                expression => expression.Ignore())
            .ForMember(userTask=>userTask.Id, expression=>expression.Ignore());

        CreateMap<UserTaskFilterDto, UserTaskFilterData>()
            .ForMember(userTaskFilter => userTaskFilter.Id, expression => expression.Ignore())
            .ReverseMap();

        CreateMap<UserTaskListDto, UserTaskList>()
            .ForMember(member=>member.UserTasks, expression => expression.Ignore())
            .ReverseMap();
        
        CreateMap<UserTaskListFilterDto, UserTaskListFilter>();

        CreateMap<BsonDocument, UserTaskFileSimpleDto>()
            .ForMember(userTaskFileSimpleDto => userTaskFileSimpleDto.Id,
                expression => expression.MapFrom(x => x.GetElement(IdFieldName).Value.ToString()))
            .ForMember(userTaskFileSimpleDto => userTaskFileSimpleDto.UserTaskId,
                expression => expression.MapFrom(x => Guid.Parse(x.GetElement(UserTaskIdFieldName).Value.ToString()!)))
            .ForMember(userTaskFileSimpleDto => userTaskFileSimpleDto.Created,
                expression => expression.MapFrom(x => x[UploadDateFieldName].AsLocalTime))
            .ForMember(userTaskFileSimpleDto => userTaskFileSimpleDto.Name,
                expression => expression.MapFrom(x => x.GetElement(NameFieldName).Value.ToString()!));
    }
}