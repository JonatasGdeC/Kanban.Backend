using AutoMapper;
using FluentValidation.Results;
using Kanban.Communication.Requests.Column;
using Kanban.Domain.Repositories;
using Kanban.Domain.Repositories.Column;
using Kanban.Domain.Services.LoggedUser;
using Kanban.Exception.ExceptionBase;

namespace Kanban.Application.UseCase.Column.Update;
using Domain.Entities;

public class UpdateColumnUseCase(
    IColumWriteRepository writeRepository,
    IColumnReadRepository readRepository,
    ILoggedUser loggedUser,
    IMapper mapper,
    IUnitOfWork unitOfWork) : IUpdateColumnUseCase
{
    public async Task Execute(Guid id, UpdateColumnRequest request)
    {
        Column column = await GetValidatedColumn(id: id, request: request);

        mapper.Map(source: request, destination: column);
        
        writeRepository.Update(column: column);
        await unitOfWork.Commit();
    }

    private async Task<Column> GetValidatedColumn(Guid id, UpdateColumnRequest request)
    {
        ColumnValidator validator = new();
        ValidationResult? result = await validator.ValidateAsync(instance: request);
        
        if (!result.IsValid)
        {
            List<string> errors = result.Errors.Select(selector: error => error.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorsMessages: errors);
        }

        User user = await loggedUser.Get();
        Column? column = await writeRepository.GetById(id: id, userId: user.Id);
        if (column == null)
        {
            throw new NotFoundException(message: "Column not found");
        }

        bool existsColumnThisPosition = await readRepository.ExistsColumnThisPosition(boardId: column.BoardId, position: request.Order, ignoreColumnId: id);
        if (existsColumnThisPosition)
        {
            throw new ErrorOnValidationException(errorsMessages: ["There is already a column in that position."]);
        }
        
        return column;
    }
}
