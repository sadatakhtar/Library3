using library_v3.Common.Results;
using library_v3.Models;
using MediatR;

namespace library_v3.Features.Books.GetAllBooks
{
    public sealed record GetAllBooksQuery() : IRequest<CommandResult<List<Book>>>;

}
