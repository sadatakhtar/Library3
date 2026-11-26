using FluentValidation;

namespace library_v3.Features.Authors.UpdateAuthor
{
    public class UpdateAuthorValidator : AbstractValidator<UpdateAuthorCommand>
    {
        public UpdateAuthorValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id cannot be empty")
                .GreaterThan(0).WithMessage("Id must be greater than 0");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name cannot be empty")
                .MinimumLength(3).WithMessage("Name must be more than 2 chars");
            RuleFor(x => x.Country).NotEmpty().WithMessage("Country must not be empty")
                .MinimumLength(2).WithMessage("country must be more than 1 char");
        }
    }
}
