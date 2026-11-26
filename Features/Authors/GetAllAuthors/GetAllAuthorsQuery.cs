using library_v3.Common.Results;
using library_v3.Models;
using MediatR;

namespace library_v3.Features.Authors.GetAllAuthors
{
    public sealed record GetAllAuthorsQuery() : IRequest<CommandResult<List<Author>>>;

}
