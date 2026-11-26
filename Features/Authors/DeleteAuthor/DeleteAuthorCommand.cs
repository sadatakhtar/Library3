using library_v3.Common.Results;
using MediatR;

namespace library_v3.Features.Authors.DeleteAuthor
{
    public sealed record DeleteAuthorCommand(int Id) : IRequest<CommandResult>;

}
