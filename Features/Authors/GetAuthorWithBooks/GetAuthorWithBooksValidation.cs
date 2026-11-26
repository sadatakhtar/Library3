using FluentValidation;
using library_v3.Features.Authors.GetAuthorsWithBooks;

namespace library_v3.Features.Authors.GetAuthorWithBooks
{
    public class GetAuthorWithBooksValidation : AbstractValidator<GetAuthorWithBooksQuery>
    {
        public GetAuthorWithBooksValidation()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required")
                .GreaterThan(0).WithMessage("Id must be greater than 0");
        }
    }
}
