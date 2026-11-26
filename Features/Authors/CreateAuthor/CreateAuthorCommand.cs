using library_v3.Common.Results;
using MediatR;

namespace library_v3.Features.Authors.CreateAuthor
{
    public sealed record CreateAuthorCommand(string Name, string Country) : IRequest<CommandResult>;
}
