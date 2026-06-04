using Kanban.Application.UseCase.Board.Delete;
using Kanban.Application.UseCase.Board.GetAll;
using Kanban.Application.UseCase.Board.GetById;
using Kanban.Application.UseCase.Board.Register;
using Kanban.Application.UseCase.Board.Update;
using Kanban.Application.UseCase.Column.Delete;
using Kanban.Application.UseCase.Column.GetAll;
using Kanban.Application.UseCase.Column.GetById;
using Kanban.Application.UseCase.Column.Register;
using Kanban.Application.UseCase.Column.Update;
using Microsoft.Extensions.DependencyInjection;

namespace Kanban.Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddAutoMapperApplication(services: services);
        AddUseCases(services: services);
    }

    private static void AddAutoMapperApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(configAction: config => { }, typeof(AutoMapping.AutoMapping));
    }

    private static void AddUseCases(this IServiceCollection services)
    {
        services.AddScoped<IDeleteBoardUseCase, DeleteBoardUseCase>();
        services.AddScoped<IGetAllBoardsUseCase, GetAllBoardsUseCase>();
        services.AddScoped<IGetBoardByIdUseCase, GetBoardByIdUseCase>();
        services.AddScoped<IRegisterBoardUseCase, RegisterBoardUseCase>();
        services.AddScoped<IUpdateBoardUseCase, UpdateBoardUseCase>();

        services.AddScoped<IDeleteColumnUseCase, DeleteColumnUseCase>();
        services.AddScoped<IGetAllColumnsUseCase, GetAllColumnsUseCase>();
        services.AddScoped<IGetColumnByIdUseCase, GetColumnByIdUseCase>();
        services.AddScoped<IRegisterColumnUseCase, RegisterColumnUseCase>();
        services.AddScoped<IUpdateColumnUseCase, UpdateColumnUseCase>();
    }
}