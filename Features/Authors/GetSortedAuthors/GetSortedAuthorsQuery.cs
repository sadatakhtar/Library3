using library_v3.Common.Results;
using library_v3.Models;
using MediatR;

namespace library_v3.Features.Authors.GetSortedAuthors
{
    public sealed record GetSortedAuthorsQuery : IRequest<CommandResult<List<Author>>>;

}
