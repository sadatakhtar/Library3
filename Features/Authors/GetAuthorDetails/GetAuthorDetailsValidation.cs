using FluentValidation;

namespace library_v3.Features.Authors.GetAuthorDetails
{
    public class GetAuthorDetailsValidation : AbstractValidator<GetAuthorDetailsQuery>
    {
        public GetAuthorDetailsValidation()
        {
            RuleFor(x => x.AuthorId).NotEmpty().WithMessage("Author Id cannot be empty")
                .GreaterThan(0).WithMessage("Author Id must be greater than 0");
        }
    }
}
