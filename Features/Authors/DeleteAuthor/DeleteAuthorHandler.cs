using FluentValidation;
using library_v3.Common.Results;
using library_v3.Providers;
using MediatR;

namespace library_v3.Features.Authors.DeleteAuthor
{
    public class DeleteAuthorHandler : IRequestHandler<DeleteAuthorCommand, CommandResult>
    {
        private readonly IAuthorProvider _authorProvider;
        private readonly IValidator<DeleteAuthorCommand> _validator;

        public DeleteAuthorHandler(IAuthorProvider authorProvider, IValidator<DeleteAuthorCommand> validator)
        {
            _authorProvider = authorProvider;
            _validator = validator;
        }

        public async Task<CommandResult> Handle(DeleteAuthorCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command);
            if (!validationResult.IsValid)
                return new CommandResult(false, validationResult.Errors.First().ErrorMessage);

            var author = await _authorProvider.GetAuthorByIdAsync(command.Id);
            if (author == null)
            {
                return new CommandResult(false, $"Author not found");
            }

            await _authorProvider.DeleteAuthor(author);
            return new CommandResult(true, null);
        }
    }
}
