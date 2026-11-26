using library_v3.Common.Results;
using MediatR;

namespace library_v3.Features.Books.DeleteBook
{
    public sealed record DeleteBookCommand(int Id) : IRequest<CommandResult>;

}
