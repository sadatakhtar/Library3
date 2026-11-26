using FluentValidation;

namespace library_v3.Features.Books.CreateBook
{
    public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
    {
        public CreateBookCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Book title is required.")
                //.MaximumLength(100)
                .MinimumLength(3)
                .WithMessage("Book title must be minimum 3 chars.");

            RuleFor(x => x.AuthorId)
                .GreaterThan(0)
                .WithMessage("A valid Author Id must be supplied.");

            RuleFor(x => x.PublishedDate)
                .NotEmpty()
                .WithMessage("Published date is required.")
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage("Published date cannot be in the future.");

        }

    }
}
