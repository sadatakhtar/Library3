using library_v3.Common.Results;
using library_v3.Models;
using MediatR;

namespace library_v3.Features.Authors.GetAuthorDetails
{
    public sealed record GetAuthorDetailsQuery(int AuthorId) : IRequest<CommandResult<Author>>;
}
