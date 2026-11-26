using FluentValidation;
using library_v3.Common.Results;
using library_v3.DTOs;
using library_v3.Providers;
using MediatR;

namespace library_v3.Features.Authors.GetAuthorStats
{
    public class GetAuthorStatsHandler
        : IRequestHandler<GetAuthorStatsQuery, CommandResult<List<AuthorStatsDTO>>>
    {
        private readonly IAuthorProvider _authorProvider;
        private readonly IValidator<GetAuthorStatsQuery> _validator;

        public GetAuthorStatsHandler(
            IAuthorProvider authorProvider,
            IValidator<GetAuthorStatsQuery> validator
        )
        {
            _authorProvider = authorProvider;
            _validator = validator;
        }

        public async Task<CommandResult<List<AuthorStatsDTO?>>> Handle(
            GetAuthorStatsQuery query,
            CancellationToken cancellationToken
        )
        {
            var validationResult = await _validator.ValidateAsync(query);
            if (!validationResult.IsValid)
            {
                return new CommandResult<List<AuthorStatsDTO>>(
                    false,
                    null,
                    validationResult.Errors.First().ErrorMessage
                );
            }

            var stats = await _authorProvider.GetAuthorStatsAsync();
            if (stats.Count == 0)
            {
                return new CommandResult<List<AuthorStatsDTO>>(
                    false,
                    null,
                    $"No author stats found"
                );
            }

            return new CommandResult<List<AuthorStatsDTO>>(true, stats, null);
        }
    }
}
