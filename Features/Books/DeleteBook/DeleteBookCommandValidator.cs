using FluentValidation;

namespace library_v3.Features.Books.DeleteBook
{
    public class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>
    {
        public DeleteBookCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("You must enter a valid book id.")
                .GreaterThan(0).WithMessage("Id must be greater than 0");


        }
    }
}
