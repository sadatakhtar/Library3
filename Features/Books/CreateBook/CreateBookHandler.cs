using FluentValidation;
using library_v3.Common.Results;
using library_v3.Models;
using library_v3.Providers;
using MediatR;

namespace library_v3.Features.Books.CreateBook
{
    public class CreateBookHandler : IRequestHandler<CreateBookCommand, CommandResult>
    {
        private readonly IBookProvider _bookProvider;
        private readonly IValidator<CreateBookCommand> _createBookValidator;

        public CreateBookHandler(
            IBookProvider bookProvider,
            IValidator<CreateBookCommand> createBookValidator
        )
        {
            _bookProvider = bookProvider;
            _createBookValidator = createBookValidator;
        }

        public async Task<CommandResult> Handle(
            CreateBookCommand command,
            CancellationToken cancellationToken
        )
        {
            var validationResult = await _createBookValidator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                return new CommandResult(false, validationResult.Errors.First().ErrorMessage);
            }

            var existingBook = await _bookProvider.BookExistsAsync(command.Title);
            if (existingBook)
                return new CommandResult(false, "Book already exists");

            var newBook = new Book
            {
                Title = command.Title,
                PublishedDate = command.PublishedDate,
                AuthorId = command.AuthorId,
            };

            await _bookProvider.CreateNewBook(newBook);
            return new CommandResult(true, null);
        }
    }
}
