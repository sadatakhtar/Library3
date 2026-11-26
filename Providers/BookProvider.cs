using library_v3.Data;
using library_v3.DTOs;
using library_v3.Models;
using Microsoft.EntityFrameworkCore;

namespace library_v3.Providers
{
    public class BookProvider : IBookProvider
    {
        private readonly LibDBContext _db;

        public BookProvider(LibDBContext db)
        {
            _db = db;
        }

        public async Task<List<Book>> GetAllBooksAsync()
        {
            return await _db.Books.ToListAsync();
        }

        public async Task<Book?> GetBookByIdAsync(int id)
        {
            return await _db.Books.FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Book?> CreateBookAsync(BookCreateDTO newBook)
        {
            return await _db.Books.FirstOrDefaultAsync(b => b.Title == newBook.Title);
        }

        public async Task<Book?> UpdateBookByIdAsync(int id, BookUpdateDTO updateBook)
        {
            return await _db.Books.FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Book?> DeleteBookAsync(int id)
        {
            var book = await _db.Books.FirstOrDefaultAsync(b => b.Id == id);
            if (book == null)
                return null;

            _db.Books.Remove(book);
            await _db.SaveChangesAsync();

            return book;
        }

        public async Task<List<Book>> SearchBooksByTitleAsync(string title)
        {
            return await _db.Books.Where(b => b.Title.Contains(title)).ToListAsync();
        }

        public async Task<List<Book?>> GetSortedBooksAsync()
        {
            return await _db.Books.OrderBy(b => b.Title).ToListAsync();
        }

        public async Task CreateNewBook(Book book)
        {
            await _db.Books.AddAsync(book);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateBook()
        {
            await _db.SaveChangesAsync();
        }

        public async Task<bool> BookExistsAsync(string title)
        {
            return await _db.Books.AnyAsync(b => b.Title == title);
        }

        public async Task<Book?> GetBookByNameAsync(string title)
        {
            return await _db.Books.FirstOrDefaultAsync(b => b.Title == title);
        }

        public async Task DeleteBook(Book book)
        {
            _db.Books.Remove(book);
            await _db.SaveChangesAsync();
        }

    }
}
