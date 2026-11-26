using FluentValidation;
using library_v3.Common.Results;
using library_v3.Models;
using library_v3.Providers;
using MediatR;

namespace library_v3.Features.Books.GetAllBooks
{
    public class GetAllBooksHandler : IRequestHandler<GetAllBooksQuery, CommandResult<List<Book>>>
    {
        private readonly IBookProvider _bookProvider;
        private readonly IValidator<GetAllBooksQuery> _validator;

        public GetAllBooksHandler(
            IBookProvider bookProvider,
            IValidator<GetAllBooksQuery> validator
        )
        {
            _bookProvider = bookProvider;
            _validator = validator;
        }

        public async Task<CommandResult<List<Book?>>> Handle(
            GetAllBooksQuery query,
            CancellationToken cancellationToken
        )
        {
            var validationResult = await _validator.ValidateAsync(query);
            if (!validationResult.IsValid)
            {
                return new CommandResult<List<Book?>>(
                    false,
                    null,
                    validationResult.Errors.First().ErrorMessage
                );
            }

            var books = await _bookProvider.GetAllBooksAsync();

            if (!books.Any())
            {
                return new CommandResult<List<Book?>>(false, null, $"No books found");
            }

            return new CommandResult<List<Book?>>(true, books, null);
        }
    }
}
