using FluentValidation;
using Kanban.Communication.Requests.Task;

namespace Kanban.Application.UseCase.TaskEntity;

public class TaskValidator : AbstractValidator<RegisterTaskRequest>
{
    public TaskValidator()
    {
        RuleFor(expression: request => request.Name)
            .NotEmpty().WithMessage(errorMessage: "Name is required")
            .MinimumLength(minimumLength: 3).WithMessage(errorMessage: "Name must be at least 3 characters.")
            .MaximumLength(maximumLength: 200).WithMessage(errorMessage: "Name must be at most 200 characters.");
        
        RuleFor(expression: request => request.Status)
            .IsInEnum().WithMessage(errorMessage: "Status must be a valid enum value.");
    }
}
