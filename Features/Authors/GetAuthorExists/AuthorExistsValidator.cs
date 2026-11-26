using FluentValidation;

namespace library_v3.Features.Authors.GetAuthorExists
{
    public class AuthorExistsValidator : AbstractValidator<GetAuthorExistsQuery>
    {
        public AuthorExistsValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id must not be empty")
                .GreaterThan(0).WithMessage("Id must be greater than 0");
        }
    }
}
