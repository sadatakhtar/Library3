using library_v3.Common.Results;
using library_v3.Models;
using MediatR;

namespace library_v3.Features.Books.GetBookById
{
    public sealed record GetBookByIdQuery(int Id) : IRequest<CommandResult<Book>>;

}
