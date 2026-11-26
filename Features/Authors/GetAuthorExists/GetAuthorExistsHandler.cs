using FluentValidation;
using library_v3.Common.Results;
using library_v3.Providers;
using MediatR;

namespace library_v3.Features.Authors.GetAuthorExists
{
    public class GetAuthorExistsHandler : IRequestHandler<GetAuthorExistsQuery, CommandResult<bool>>
    {
        private readonly IAuthorProvider _authorProvider;
        private readonly IValidator<GetAuthorExistsQuery> _validator;

        public GetAuthorExistsHandler(IAuthorProvider authorProvider, IValidator<GetAuthorExistsQuery> validator)
        {
            _authorProvider = authorProvider;
            _validator = validator;
        }

        public async Task<CommandResult<bool>> Handle(GetAuthorExistsQuery query, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(query);
            if (!validationResult.IsValid)
            {
                return new CommandResult<bool>(false, false, validationResult.Errors.First().ErrorMessage);
            }

            var authorConfirmation = await _authorProvider.AuthorExistsAsync(query.Id);
            if (authorConfirmation == null)
            {
                return new CommandResult<bool>(false, false, $"No author found");
            }

            return new CommandResult<bool>(true, authorConfirmation, null);
            // return await _authorProvider.AuthorExistsAsync(query.Id);
        }
    }
}
