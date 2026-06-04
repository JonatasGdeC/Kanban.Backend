using FluentValidation;
using Kanban.Communication.Requests.SubTask;

namespace Kanban.Application.UseCase.SubTask;

public class SubTaskValidator : AbstractValidator<RegisterSubTaskRequest>
{
    public SubTaskValidator()
    {
        RuleFor(expression: request => request.Name)
            .NotEmpty().WithMessage(errorMessage: "Name is required")
            .MinimumLength(minimumLength: 3).WithMessage(errorMessage: "Name must be at least 3 characters.")
            .MaximumLength(maximumLength: 200).WithMessage(errorMessage: "Name must be at most 200 characters.");
    }
}
