using FluentValidation;
using library_v3.Common.Results;
using library_v3.Models;
using library_v3.Providers;
using MapsterMapper;
using MediatR;

namespace library_v3.Features.Authors.CreateAuthor
{
    public class CreateAuthorHandler : IRequestHandler<CreateAuthorCommand, CommandResult>
    {
        private readonly IAuthorProvider _authorProvider;
        private readonly IValidator<CreateAuthorCommand> _validator;
        private readonly IMapper _mapper;

        public CreateAuthorHandler(
            IAuthorProvider authorProvider,
            IMapper mapper,
            IValidator<CreateAuthorCommand> validator
        )
        {
            _authorProvider = authorProvider;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<CommandResult> Handle(
            CreateAuthorCommand command,
            CancellationToken cancellationToken
        )
        {
            var validationResult = await _validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return new CommandResult(false, validationResult.Errors.First().ErrorMessage);
            }

            var existingAuthor = await _authorProvider.GetByNameAsync(command.Name);
            if (existingAuthor != null)
                return new CommandResult(false, $"Author already exists");

            var author = new Author { Name = command.Name, Country = command.Country };

            await _authorProvider.CreateAuthorAsync(author);
            return new CommandResult(true, null);
        }
    }
}
