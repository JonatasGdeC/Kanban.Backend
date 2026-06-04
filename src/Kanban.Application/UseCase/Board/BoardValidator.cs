using FluentValidation;
using Kanban.Communication.Requests.Board;

namespace Kanban.Application.UseCase.Board;

public class BoardValidator : AbstractValidator<RegisterBoardRequest>
{
    public BoardValidator()
    {
        RuleFor(expression: request => request.Name)
            .NotEmpty().WithMessage(errorMessage: "Name is required")
            .MinimumLength(minimumLength: 3).WithMessage(errorMessage: "Name must be at least 3 characters.")
            .MaximumLength(maximumLength: 500).WithMessage(errorMessage: "Name must be at most 500 characters.");
    }
}