using Microsoft.EntityFrameworkCore;
using ToDo.WebApi.Domain.Entities;

namespace ToDo.WebApi.Domain;

/// <summary>
/// Контекст для работы с EntityFramework
/// </summary>
public class DataContext:DbContext 
{
    /// <summary>
    /// Пользовательские задачи
    /// </summary>
    public DbSet<UserTask> UserTasks { get; set; }

    /// <summary>
    /// Файлы пользователя
    /// </summary>
    public DbSet<UserTaskFile> UserTaskFiles { get; set; }

    /// <summary>
    /// Списки задач
    /// </summary>
    public DbSet<UserTaskList> UserTaskLists { get; set; }

    public DataContext(DbContextOptions<DataContext> options) : base(options) 
    {
        
    }

    /// <summary>
    /// Дополнительная инициализация
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserTask>().HasQueryFilter(task => !task.IsDeleted);
        modelBuilder.Entity<UserTaskList>().HasQueryFilter(task => !task.IsDeleted);
        
        base.OnModelCreating(modelBuilder);
    }
}