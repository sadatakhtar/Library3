using FluentValidation;
using library_v3.Common.Results;
using library_v3.Models;
using library_v3.Providers;
using MediatR;

namespace library_v3.Features.Authors.GetAuthorDetails
{
    public class GetAuthorDetailsHandler : IRequestHandler<GetAuthorDetailsQuery, CommandResult<Author>>
    {
        private readonly IAuthorProvider _authorProvider;
        private readonly IValidator<GetAuthorDetailsQuery> _validator;

        public GetAuthorDetailsHandler(IAuthorProvider authorProvider, IValidator<GetAuthorDetailsQuery> validator)
        {
            _authorProvider = authorProvider;
            _validator = validator;
        }

        public async Task<CommandResult<Author?>> Handle(GetAuthorDetailsQuery query, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(query);
            if (!validationResult.IsValid)
            {
                return new CommandResult<Author>(false, null, validationResult.Errors.First().ErrorMessage);
            }

            var authorDetails = await _authorProvider.GetAuthorWithBooksAsync(query.AuthorId);

            if (authorDetails == null)
            {
                return new CommandResult<Author>(false, null, $"No author details found");
            }

            return new CommandResult<Author>(true, authorDetails, null);

            //  return await _authorProvider.GetAuthorWithBooksAsync(query.AuthorId);
        }

    }
}
