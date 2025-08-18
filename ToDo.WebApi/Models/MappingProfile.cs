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
        
        CreateMap<UserTaskDto, UserTaskModel>();
        
        CreateMap<UserTaskDto, UserTaskSmallModel>();

        CreateMap<UserTaskListAddModel, UserTaskListDto>();
        
        CreateMap<UserTaskListDto, UserTaskListModel>();
    }
}