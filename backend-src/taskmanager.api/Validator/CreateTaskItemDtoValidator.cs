using FluentValidation;
using taskmanager.api.Models;

namespace taskmanager.api.Validator
{
    public class CreateTaskItemDtoValidator : AbstractValidator<CreateTaskItemDto>
    {
        public CreateTaskItemDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.");
        }
    }
}
