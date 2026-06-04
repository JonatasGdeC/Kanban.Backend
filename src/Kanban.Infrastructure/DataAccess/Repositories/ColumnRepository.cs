using Kanban.Domain.Entities;
using Kanban.Domain.Repositories.Column;
using Microsoft.EntityFrameworkCore;

namespace Kanban.Infrastructure.DataAccess.Repositories;

public class ColumnRepository(KanbanDbContext context) : IColumnReadRepository, IColumWriteRepository
{
    public async Task<List<Column>> GetAll(Guid boardId, Guid userId)
    {
        return await context.Columns.AsNoTracking().Where(predicate: column => column.BoardId == boardId && column.Board.UserId == userId).ToListAsync();
    }

    public async Task Add(Column column)
    {
        await context.Columns.AddAsync(entity: column);
    }

    public void Update(Column column)
    {
        context.Columns.Update(entity: column);
    }

    public void Delete(Column column)
    {
        context.Columns.Remove(entity: column);
    }

    async Task<Column?> IColumWriteRepository.GetById(Guid id, Guid userId)
    {
        return await context.Columns.FirstOrDefaultAsync(predicate: column => column.Id == id && column.Board.UserId == userId);
    }

    public async Task<bool> ExistsColumnInPosition (Guid boardId, int position, Guid ignoreColumnId)
    {
        return await context.Columns.AsNoTracking().AnyAsync(predicate: column => column.BoardId == boardId && column.Order == position && column.Id != ignoreColumnId);
    }

    async Task<Column?> IColumnReadRepository.GetById(Guid id, Guid userId)
    {
        return await context.Columns.AsNoTracking()
            .Include(navigationPropertyPath: column => column.Tasks)
            .FirstOrDefaultAsync(predicate: column => column.Id == id && column.Board.UserId == userId);
    }
}
