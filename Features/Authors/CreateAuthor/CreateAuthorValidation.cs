using FluentValidation;

namespace library_v3.Features.Authors.CreateAuthor
{
    public class CreateAuthorValidation : AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name cannot be empty")
                .MinimumLength(3).WithMessage("Name must be more than 2 chars");

            RuleFor(x => x.Country).NotEmpty().WithMessage("must provide country")
                .MinimumLength(2).WithMessage("minimum length for country name is 2");
        }
    }
}
