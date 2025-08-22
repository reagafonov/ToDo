using AutoMapper;
using ToDo.WebApi.ServiceDomain;

namespace ToDo.WebApi.Models;

/// <summary>
/// Настройки автомапера для моделей
/// </summary>
public class MappingProfile:Profile
{
    public MappingProfile()
    {
        CreateMap<UserTaskAddModel, UserTaskDto>();

        CreateMap<UserTaskUpdateModel, UserTaskDto>()
            .ForMember(dto=>dto.IsCompleted, opt=>opt.MapFrom(model=>  !string.IsNullOrWhiteSpace(model.IsCompleted) && model.IsCompleted.ToLower().Trim() != "false"));

        CreateMap<UserTaskDto, UserTaskModel>();
        
        CreateMap<UserTaskDto, UserTaskSmallModel>();

        CreateMap<UserTaskListAddModel, UserTaskListDto>();
        
        CreateMap<UserTaskListDto, UserTaskListModel>();

        CreateMap<UserTaskFileSimpleDto, UserTaskFileModel>();

        CreateMap<IFormFile, UserTaskFileDto>()
            .ForMember(dto => dto.Name, expression => expression.MapFrom(file => file.Name))
            .ForMember(dto => dto.Id, expression => expression.MapFrom(file => Guid.NewGuid()))
            .ForMember(dto => dto.Created, expression => expression.MapFrom(file => DateTime.Now))
            .ForMember(dto => dto.UserTaskId, expression => expression.Ignore())
            .ForMember(dto => dto.Contents, expression => expression.Ignore());
    }
}