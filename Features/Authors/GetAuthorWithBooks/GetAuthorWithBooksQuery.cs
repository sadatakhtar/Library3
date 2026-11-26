using library_v3.Common.Results;
using library_v3.Models;
using MediatR;

namespace library_v3.Features.Authors.GetAuthorsWithBooks
{
    public sealed record GetAuthorWithBooksQuery(int Id) : IRequest<CommandResult<List<Book>>>;

}
