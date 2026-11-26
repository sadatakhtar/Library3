using FluentValidation;
using library_v3.Common.Results;
using library_v3.Models;
using library_v3.Providers;
using MediatR;

namespace library_v3.Features.Books.GetBookById
{
    public class GetBookByIdHandler : IRequestHandler<GetBookByIdQuery, CommandResult<Book>>

    {
        private readonly IBookProvider _bookProvider;
        private readonly IValidator<GetBookByIdQuery> _validator;

        public GetBookByIdHandler(
            IBookProvider bookProvider,
            IValidator<GetBookByIdQuery> validator
        )
        {
            _bookProvider = bookProvider;
            _validator = validator;
        }

        public async Task<CommandResult<Book>> Handle(GetBookByIdQuery query, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(query);
            if (!validationResult.IsValid)
            {
                return new CommandResult<Book>(false, null, $"Book with ID {query.Id} not found.");
            }

            var book = await _bookProvider.GetBookByIdAsync(query.Id);
            if (book == null)
            {
                return new CommandResult<Book>(false, null, $"Book with ID {query.Id} not found.");
            }

            return new CommandResult<Book>(true, book, null);
        }
    }
}
