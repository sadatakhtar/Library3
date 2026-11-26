using library_v3.Common.Results;
using MediatR;

namespace library_v3.Features.Authors.GetAuthorExists
{
    public sealed record GetAuthorExistsQuery(int Id) : IRequest<CommandResult<bool>>;

}
