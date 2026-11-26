using FluentValidation;

namespace library_v3.Features.Books.UpdateBook
{
    public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Book Id cannot be empty");

            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Title cannot be empty")
                .MinimumLength(3)
                .WithMessage("Title must be minimum 3 characters")
                .MaximumLength(100)
                .WithMessage("Title cannot exceed 100 characters");

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
