using library_v3.Common.Results;
using library_v3.Models;
using MediatR;

namespace library_v3.Features.Authors.GetAuthorById
{
    public sealed record GetAuthorByIdQuery(int Id) : IRequest<CommandResult<Author>>;

}
