using FluentValidation;
using library_v3.Common.Results;
using library_v3.Providers;
using MediatR;

namespace library_v3.Features.Books.DeleteBook
{
    public class DeleteBookHandler : IRequestHandler<DeleteBookCommand, CommandResult>
    {
        private readonly IBookProvider _bookProvider;
        private readonly IValidator<DeleteBookCommand> _delBookValidator;

        public DeleteBookHandler(
            IBookProvider bookProvider,
            IValidator<DeleteBookCommand> delBookValidator
        )
        {
            _bookProvider = bookProvider;
            _delBookValidator = delBookValidator;
        }

        public async Task<CommandResult> Handle(DeleteBookCommand command, CancellationToken cancellationToken)
        {
            var validatorResult = await _delBookValidator.ValidateAsync(command);
            if (!validatorResult.IsValid)
            {
                return new CommandResult(false, validatorResult.Errors.First().ErrorMessage);
            }

            var bookToDelete = await _bookProvider.GetBookByIdAsync(command.Id);
            if (bookToDelete == null)
                return new CommandResult(false, $"Cannot find book to delete with ID {command.Id}");

            await _bookProvider.DeleteBook(bookToDelete);
            return new CommandResult(true, null);
        }
    }
}
