using Kanban.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kanban.Infrastructure.DataAccess;

public class KanbanDbContext(DbContextOptions options) : DbContext(options: options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Board> Boards { get; set; }
    public DbSet<Column> Columns { get; set; }
    public DbSet<TaskEntity> Tasks { get; set; }
    public DbSet<SubTask> SubTasks { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder: modelBuilder);

        modelBuilder.Entity<Board>()
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(foreignKeyExpression: b => b.UserId)
            .OnDelete(deleteBehavior: DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Board>()
            .HasMany(navigationExpression: b => b.Columns)
            .WithOne()
            .HasForeignKey(foreignKeyExpression: c => c.BoardId)
            .OnDelete(deleteBehavior: DeleteBehavior.Cascade);

        modelBuilder.Entity<Column>()
            .HasMany(navigationExpression: c => c.Tasks)
            .WithOne()
            .HasForeignKey(foreignKeyExpression: t => t.ColumnId)
            .OnDelete(deleteBehavior: DeleteBehavior.Cascade);

        modelBuilder.Entity<TaskEntity>()
            .HasMany(navigationExpression: t => t.SubTasks)
            .WithOne()
            .HasForeignKey(foreignKeyExpression: s => s.TaskId)
            .OnDelete(deleteBehavior: DeleteBehavior.Cascade);
    }
}