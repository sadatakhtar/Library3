using library_v3.Common.Results;
using library_v3.DTOs;
using MediatR;


namespace library_v3.Features.Authors.GetAuthorStats
{
    public sealed record GetAuthorStatsQuery() : IRequest<CommandResult<List<AuthorStatsDTO>>>;

}
