using FluentValidation;
using library_v3.Common.Results;
using library_v3.Features.Authors.GetAuthorsWithBooks;
using library_v3.Models;
using library_v3.Providers;
using MediatR;

namespace library_v3.Features.Authors.GetAuthorWithBooks
{
    public class GetAuthorWithBooksHandler
        : IRequestHandler<GetAuthorWithBooksQuery, CommandResult<List<Book>>>
    {
        private readonly IAuthorProvider _authorProvider;
        private readonly IValidator<GetAuthorWithBooksQuery> _validator;

        public GetAuthorWithBooksHandler(
            IAuthorProvider authorProvider,
            IValidator<GetAuthorWithBooksQuery> validate
        )
        {
            _authorProvider = authorProvider;
            _validator = validate;
        }

        public async Task<CommandResult<List<Book>>> Handle(
            GetAuthorWithBooksQuery query,
            CancellationToken cancellationToken
        )
        {
            var validationResult = await _validator.ValidateAsync(query);
            if (!validationResult.IsValid)
            {
                return new CommandResult<List<Book>>(
                    false,
                    null,
                    validationResult.Errors.First().ErrorMessage
                );
            }

            var authorsWithBooks = await _authorProvider.GetBooksByAuthorIdAsync(query.Id);
            if (authorsWithBooks == null)
            {
                return new CommandResult<List<Book>>(false, null, $"No authors with books found");
            }

            return new CommandResult<List<Book>>(true, authorsWithBooks, null);
        }
    }
}
