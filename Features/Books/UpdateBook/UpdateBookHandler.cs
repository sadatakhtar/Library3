using FluentValidation;
using library_v3.Common.Results;
using library_v3.Providers;
using MediatR;

namespace library_v3.Features.Books.UpdateBook
{
    public class UpdateBookHandler : IRequestHandler<UpdateBookCommand, CommandResult>
    {
        private readonly IBookProvider _bookProvider;
        private readonly IValidator<UpdateBookCommand> _updateBookValidator;

        public UpdateBookHandler(
            IBookProvider bookProvider,
            IValidator<UpdateBookCommand> updateBookValidator
        )
        {
            _bookProvider = bookProvider;
            _updateBookValidator = updateBookValidator;
        }

        public async Task<CommandResult> Handle(UpdateBookCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await _updateBookValidator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return new CommandResult(false, validationResult.Errors.First().ErrorMessage);
            }

            var existingBook = await _bookProvider.GetBookByIdAsync(command.Id);

            if (existingBook == null)
            {
                return new CommandResult(false, $"Book with ID {command.Id} not found (H)");
            }

            existingBook.Title = command.Title;
            existingBook.PublishedDate = command.PublishedDate;
            existingBook.AuthorId = command.AuthorId;

            await _bookProvider.UpdateBook();
            return new CommandResult(true, null);
        }
    }
}
