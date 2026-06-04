using AutoMapper;
using FluentValidation.Results;
using Kanban.Communication.Dtos;
using Kanban.Communication.Requests.Board;
using Kanban.Domain.Repositories;
using Kanban.Domain.Repositories.Boad;
using Kanban.Exception.ExceptionBase;

namespace Kanban.Application.UseCase.Board.Register;
using Domain.Entities;

public class RegisterBoardUseCase(
    IBoardReadRepository readRepository, 
    IBoardWriteRepository whiteRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRegisterBoardUseCase
{
    public async Task<BoardDto> Execute(RegisterBoardRequest request)
    {
        await Validate(request: request);
        
        Board board = mapper.Map<Board>(source: request);
        await whiteRepository.Add(board: board);
        await unitOfWork.Commit();
        
        return mapper.Map<BoardDto>(source: board);
    }

    private async Task Validate(RegisterBoardRequest request)
    {
        BoardValidator boardValidator = new();
        ValidationResult? result = boardValidator.Validate(instance: request);

        Board? boardExists = await readRepository.GetByTitle(title: request.Name, userId: Guid.NewGuid());
        if (boardExists != null)
        {
            result.Errors.Add(item: new ValidationFailure(propertyName: string.Empty, errorMessage: "Board already exists"));
        }
        
        
        if (!result.IsValid)
        {
            List<string> errors = result.Errors.Select(selector: error => error.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorsMessages: errors); 
        }
    }
}