using AutoMapper;
using Kanban.Communication.Dtos;
using Kanban.Communication.Requests.Board;
using Kanban.Communication.Requests.Column;
using Kanban.Communication.Requests.Task;

namespace Kanban.Application.AutoMapping;
using Domain.Entities;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToEntity();
        EntityToResponse();
    }

    private void RequestToEntity()
    {
        CreateMap<RegisterBoardRequest, Board>();
        CreateMap<RegisterColumnRequest, Column>();
        CreateMap<UpdateColumnRequest, Column>();
        CreateMap<RegisterTaskRequest, TaskEntity>();
        CreateMap<UpdateTaskRequest, TaskEntity>();
    }
    
    private void EntityToResponse()
    {
        CreateMap<Board, BoardDto>();
        CreateMap<Column, ColumnDto>();
        CreateMap<TaskEntity, TaskDto>();
        CreateMap<SubTask, SubTaskDto>();
    }
}
