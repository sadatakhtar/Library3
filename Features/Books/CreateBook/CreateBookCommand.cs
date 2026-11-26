using library_v3.Common.Results;
using MediatR;

namespace library_v3.Features.Books.CreateBook
{
    public sealed record CreateBookCommand(
        string Title,
        DateTime PublishedDate,
        int AuthorId
    ) : IRequest<CommandResult>;

}
