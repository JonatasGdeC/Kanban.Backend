using Kanban.Domain.Repositories.Boad;
using Kanban.Domain.Repositories.Column;
using Kanban.Domain.Repositories.SubTask;
using Kanban.Domain.Repositories.Task;
using Kanban.Infrastructure.DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Kanban.Infrastructure;

public static class DependencyInjection
{
     public static void AddInfrastructure(this IServiceCollection services)
    {
        AddRepositories(services: services);
    }
     
    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IBoardReadRepository, BoardRepository>();
        services.AddScoped<IBoardWriteRepository, BoardRepository>();
        services.AddScoped<IColumnReadRepository, ColumnRepository>();
        services.AddScoped<IColumWriteRepository, ColumnRepository>();
        services.AddScoped<ITaskReadRepository, TaskRepository>();
        services.AddScoped<ITaskWriteRepository, TaskRepository>();
        services.AddScoped<ISubTaskReadRepository, SubTaskRepository>();
        services.AddScoped<ISubTaskWriteRepository, SubTaskRepository>();
    }
}