using AutoMapper;

namespace ToDo.Models;

/// <summary>
/// Настройки автомапера для моделей
/// </summary>
public class MappingProfile:Profile
{
    public MappingProfile()
    {
        CreateMap<UserTaskListModel, UserTaskListAddModel>();
    }
}