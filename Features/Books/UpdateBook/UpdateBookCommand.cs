using library_v3.Common.Results;
using MediatR;

namespace library_v3.Features.Books.UpdateBook
{
    public sealed record UpdateBookCommand(
        int Id,
        string Title,
        DateTime PublishedDate,
        int AuthorId
        ) : IRequest<CommandResult>;

}
