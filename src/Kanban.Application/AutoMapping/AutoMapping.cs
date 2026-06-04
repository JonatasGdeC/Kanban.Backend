using AutoMapper;
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
    }
}
