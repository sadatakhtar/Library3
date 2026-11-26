using FluentValidation;

namespace library_v3.Features.Authors.GetAuthorById
{
    public class GetAuthorByIdValidator : AbstractValidator<GetAuthorByIdQuery>
    {
        public GetAuthorByIdValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Author ID cannot be empty")
                .GreaterThan(0).WithMessage("Id must be greater than 0");
        }
    }
}
