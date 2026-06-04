namespace Kanban.Domain.Repositories.Column;
using Entities;

public interface IColumnReadRepository
{
    Task<List<Column>> GetAll(Guid boardId, Guid userId);
    Task<Column?> GetById(Guid id, Guid userId);
    Task<bool> ExistsColumnThisPosition(Guid boardId, int position, Guid ignoreColumnId);
}