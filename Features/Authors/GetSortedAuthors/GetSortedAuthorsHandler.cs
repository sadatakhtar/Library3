using FluentValidation;
using library_v3.Common.Results;
using library_v3.Models;
using library_v3.Providers;
using MediatR;

namespace library_v3.Features.Authors.GetSortedAuthors
{
    public class GetSortedAuthorsHandler : IRequestHandler<GetSortedAuthorsQuery, CommandResult<List<Author>>>
    {
        private readonly IAuthorProvider _authorProvider;
        private readonly IValidator<GetSortedAuthorsQuery> _validate;

        public GetSortedAuthorsHandler(IAuthorProvider authorProvider, IValidator<GetSortedAuthorsQuery> validator)
        {
            _authorProvider = authorProvider;
            _validate = validator;
        }

        public async Task<CommandResult<List<Author>>> Handle(GetSortedAuthorsQuery query, CancellationToken cancellationToken)
        {
            var validationResult = await _validate.ValidateAsync(query);
            if (!validationResult.IsValid)
            {
                return new CommandResult<List<Author>>(false, null, validationResult.Errors.First().ErrorMessage);
            }

            var sortedAuthors = await _authorProvider.GetSortedAuthorsAsync();
            if (!sortedAuthors.Any())
            {
                return new CommandResult<List<Author>>(false, null, $"No sorted authors found.");
            }

            return new CommandResult<List<Author>>(true, sortedAuthors, null);

        }
    }
}
