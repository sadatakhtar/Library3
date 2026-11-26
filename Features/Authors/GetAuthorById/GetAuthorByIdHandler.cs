using FluentValidation;
using library_v3.Common.Results;
using library_v3.Models;
using library_v3.Providers;
using MediatR;

namespace library_v3.Features.Authors.GetAuthorById
{
    public class GetAuthorByIdHandler : IRequestHandler<GetAuthorByIdQuery, CommandResult<Author>>
    {
        private readonly IAuthorProvider _authorProvider;
        private readonly IValidator<GetAuthorByIdQuery> _validator;

        public GetAuthorByIdHandler(IAuthorProvider authorProvider, IValidator<GetAuthorByIdQuery> validator)
        {
            _authorProvider = authorProvider;
            _validator = validator;

        }

        public async Task<CommandResult<Author>> Handle(GetAuthorByIdQuery query, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(query);
            if (!validationResult.IsValid)
            {
                return new CommandResult<Author>(false, null, validationResult.Errors.First().ErrorMessage);
            }

            var author = await _authorProvider.GetAuthorByIdAsync(query.Id);
            if (author == null)
            {
                return new CommandResult<Author>(false, null, $"Author with ID {query.Id} not found.");
            }

            return new CommandResult<Author>(true, author, null);
        }
    }
}
