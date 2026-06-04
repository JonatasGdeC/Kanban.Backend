using Kanban.Domain.Repositories;
using Kanban.Domain.Repositories.Boad;
using Kanban.Domain.Repositories.Column;
using Kanban.Domain.Repositories.SubTask;
using Kanban.Domain.Repositories.Task;
using Kanban.Infrastructure.DataAccess;
using Kanban.Infrastructure.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kanban.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfigurationManager configurationManager)
    {
        AddRepositories(services: services);
        AddDbContext(services: services, connectionString: configurationManager.GetConnectionString(name: "connection")!);
    }

    private static void AddDbContext(IServiceCollection services, string connectionString)
    {
        services.AddDbContext<KanbanDbContext>(optionsAction: options => options.UseNpgsql(connectionString: connectionString));
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
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