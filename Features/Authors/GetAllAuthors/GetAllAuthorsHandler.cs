using FluentValidation;
using library_v3.Common.Results;
using library_v3.Models;
using library_v3.Providers;
using MediatR;

namespace library_v3.Features.Authors.GetAllAuthors
{
    public class GetAllAuthorsHandler : IRequestHandler<GetAllAuthorsQuery, CommandResult<List<Author>>>
    {
        private readonly IAuthorProvider _authorProvider;
        private readonly IValidator<GetAllAuthorsQuery> _validator;

        public GetAllAuthorsHandler(
            IAuthorProvider authorProvider,
            IValidator<GetAllAuthorsQuery> validator
        )
        {
            _authorProvider = authorProvider;
            _validator = validator;
        }

        public async Task<CommandResult<List<Author>>> Handle(
            GetAllAuthorsQuery query,
            CancellationToken cancellationToken
        )
        {
            var validationResult = await _validator.ValidateAsync(query);
            if (!validationResult.IsValid)
            {
                return new CommandResult<List<Author>>(
                    false,
                    null,
                    validationResult.Errors.First().ErrorMessage
                );
            }

            var authors = await _authorProvider.GetAllAuthorsAsync();
            if (!authors.Any())
            {
                return new CommandResult<List<Author?>>(false, null, $"No authors found.");
            }

            return new CommandResult<List<Author>>(true, authors, null);
        }

    }
}
