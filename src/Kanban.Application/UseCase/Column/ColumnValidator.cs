using FluentValidation;
using Kanban.Communication.Requests.Column;

namespace Kanban.Application.UseCase.Column;

public class ColumnValidator : AbstractValidator<RegisterColumnRequest>
{
    public ColumnValidator()
    {
        RuleFor(expression: request => request.Name)
            .NotEmpty().WithMessage(errorMessage: "Name is required")
            .MinimumLength(minimumLength: 3).WithMessage(errorMessage: "Name must be at least 3 characters.")
            .MaximumLength(maximumLength: 200).WithMessage(errorMessage: "Name must be at most 200 characters.");
        
        RuleFor(expression: request => request.Color)
            .NotEmpty().WithMessage(errorMessage: "Color is required")
            .Matches(expression: @"^#([0-9A-Fa-f]{6}|[0-9A-Fa-f]{3})$")
            .WithMessage(errorMessage: "Color must be a valid hex color (e.g. #FFF or #FFFFFF)");
    }
}