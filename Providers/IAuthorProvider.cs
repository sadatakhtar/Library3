using library_v3.DTOs;
using library_v3.Models;
using AuthorStatsDTO = library_v3.DTOs.AuthorStatsDTO;

namespace library_v3.Providers
{
    public interface IAuthorProvider
    {
        Task<Author?> GetAuthorWithBooksAsync(int id);
        Task<List<Author>> GetAllAuthorsAsync();
        Task<Author?> GetAuthorByIdAsync(int id);
        Task<Author?> UpdateAuthorByIdAsync(int id);
        Task<Author?> CreateAuthorAsync(AuthorCreateDTO createAuthor);
        Task<Author> DeleteAuthorAsync(int id);
        Task<List<Book>> GetBooksByAuthorIdAsync(int id);
        Task<List<Author?>> SearchAuthorByNameAsync(string name);
        Task<Author?> GetByNameAsync(string name);
        Task<List<AuthorStatsDTO>> GetAuthorStatsAsync();
        Task<bool> AuthorExistsAsync(int id);
        Task<List<Author>> GetSortedAuthorsAsync();
        Task CreateAuthorAsync(Author author);
        Task<Author?> UpdateAuthorAsync(int id, string name, string country);
        Task DeleteAuthor(Author author);

    }
}
