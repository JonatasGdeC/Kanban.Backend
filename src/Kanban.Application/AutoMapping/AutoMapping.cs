using AutoMapper;
using Kanban.Communication.Dtos;
using Kanban.Communication.Requests.Board;

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
    }
    
    private void EntityToResponse()
    {
        CreateMap<Board, BoardDto>();
        CreateMap<Column, ColumnDto>();
        CreateMap<TaskEntity, TaskDto>();
        CreateMap<SubTask, SubTaskDto>();
    }
}
