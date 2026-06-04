using Kanban.Communication.Enums;

namespace Kanban.Communication.Requests.Task;

public record RegisterTaskRequest
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public TaskStatusDto Status { get; set; }
}
