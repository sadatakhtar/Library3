using library_v3.DTOs;
using library_v3.Models;

namespace library_v3.Providers
{
    public interface IBookProvider
    {
        Task<List<Book>> GetAllBooksAsync();
        Task<Book?> GetBookByIdAsync(int id);
        Task<Book?> CreateBookAsync(BookCreateDTO newBook);
        Task<Book?> UpdateBookByIdAsync(int id, BookUpdateDTO updateBook);
        Task<Book?> DeleteBookAsync(int id);
        Task<List<Book?>> SearchBooksByTitleAsync(string title);
        Task<List<Book?>> GetSortedBooksAsync();
        Task CreateNewBook(Book book);
        Task<bool> BookExistsAsync(string title);
        Task<Book> GetBookByNameAsync(string title);
        Task UpdateBook();
        Task DeleteBook(Book book);
    }
}
