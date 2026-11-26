using FluentValidation;

namespace library_v3.Features.Books.GetBookById
{
    public class GetBookByIdCommandValidator : AbstractValidator<GetBookByIdQuery>
    {
        public GetBookByIdCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("You must provide Id")
                .GreaterThan(0).WithMessage("Id must be greater than zero");
        }

    }
}
