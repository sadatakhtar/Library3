using FluentValidation;
using library_v3.Common.Results;
using library_v3.Providers;
using MediatR;

namespace library_v3.Features.Authors.UpdateAuthor
{
    public class UpdateAuthorHandler : IRequestHandler<UpdateAuthorCommand, CommandResult>
    {
        private readonly IAuthorProvider _authorProvider;
        private readonly IValidator<UpdateAuthorCommand> _validator;

        public UpdateAuthorHandler(
            IAuthorProvider authorProvider,
            IValidator<UpdateAuthorCommand> validator
        )
        {
            _authorProvider = authorProvider;
            _validator = validator;
        }

        public async Task<CommandResult> Handle(UpdateAuthorCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return new CommandResult(
                    false,
                    //  null,
                    validationResult.Errors.First().ErrorMessage
                );
            }

            var author = await _authorProvider.UpdateAuthorAsync(
                command.Id,
                command.Name,
                command.Country
            );
            if (author == null)
            {
                return new CommandResult(
                    false,
                    //  null,
                    $"Author with ID {command.Id} not found"
                );
            }

            return new CommandResult(true, null);
        }
    }
}
