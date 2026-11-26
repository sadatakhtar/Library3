using library_v3.Common.Results;
using library_v3.Models;
using MediatR;

namespace library_v3.Features.Authors.GetSearchAuthor
{
    public sealed record GetSearchAuthorQuery(string Name) : IRequest<CommandResult<List<Author>>>;

}
