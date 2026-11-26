using library_v3.Data;
using library_v3.DTOs;
using library_v3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace library_v3.Providers
{
    public class AuthorProvider : IAuthorProvider
    {
        private readonly LibDBContext _db;

        public AuthorProvider(LibDBContext db)
        {
            _db = db;
        }

        public async Task<Author?> GetAuthorWithBooksAsync(int id)
        {
            return await _db.Authors.Include(a => a.Books).FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<List<Author>> GetAllAuthorsAsync()
        {
            return await _db.Authors.ToListAsync();
        }

        public async Task<Author?> GetAuthorByIdAsync(int id)
        {
            return await _db.Authors.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Author?> UpdateAuthorByIdAsync(int id)
        {
            return await _db.Authors.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Author?> CreateAuthorAsync([FromBody] AuthorCreateDTO createAuthor)
        {
            return await _db.Authors.FirstOrDefaultAsync(a =>
                a.Name == createAuthor.Name && a.Country == createAuthor.Country
            );
        }

        public async Task<Author?> DeleteAuthorAsync(int id)
        {
            var author = await _db.Authors.FirstOrDefaultAsync(a => a.Id == id);
            if (author == null)
                return null;

            _db.Authors.Remove(author);
            await _db.SaveChangesAsync();

            return author;
        }

        public async Task<List<Book>> GetBooksByAuthorIdAsync(int id)
        {
            return await _db.Books.Where(b => b.AuthorId == id).ToListAsync();
        }

        public async Task<List<Author?>> SearchAuthorByNameAsync(string name)
        {
            return await _db.Authors.Where(a => a.Name.Contains(name)).ToListAsync();
        }

        public async Task<List<AuthorStatsDTO>> GetAuthorStatsAsync()
        {
            return await _db
                .Authors.Select(a => new AuthorStatsDTO
                {
                    AuthorId = a.Id,
                    AuthorName = a.Name,
                    BookCount = a.Books.Count,
                })
                .ToListAsync();
        }

        public async Task<bool> AuthorExistsAsync(int id)
        {
            return await _db.Authors.AnyAsync(a => a.Id == id);
        }

        public async Task<List<Author>> GetSortedAuthorsAsync()
        {
            return await _db.Authors.OrderBy(a => a.Name).ToListAsync();
        }

        public async Task<Author?> GetByNameAsync(string name)
        {
            return await _db.Authors.FirstOrDefaultAsync(a => a.Name == name);
        }

        public async Task CreateAuthorAsync(Author author)
        {
            await _db.Authors.AddAsync(author);
            await _db.SaveChangesAsync();
        }

        public async Task<Author?> UpdateAuthorAsync(int id, string name, string country)
        {
            var aurthor = await _db.Authors.FirstOrDefaultAsync(a => a.Id == id);
            if (aurthor == null)
                return null;

            aurthor.Name = name;
            aurthor.Country = country;
            await _db.SaveChangesAsync();
            return aurthor;

        }

        public async Task DeleteAuthor(Author author)
        {
            _db.Authors.Remove(author);
            await _db.SaveChangesAsync();

        }
    }
}
