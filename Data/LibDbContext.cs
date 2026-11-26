using library_v3.Models;
using Microsoft.EntityFrameworkCore;

namespace library_v3.Data
{
    public class LibDBContext(DbContextOptions<LibDBContext> options) : DbContext(options)
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<Author>()
                .HasData(
                    new Author { Id = 1, Name = "Author 1", Country = "Country 1", Books = new List<Book>() },
                    new Author { Id = 2, Name = "Author 2", Country = "Country 2", Books = new List<Book>() },
                    new Author { Id = 3, Name = "Author 3", Country = "Country 3", Books = new List<Book>() }
                );
            modelBuilder
                .Entity<Book>()
                .HasData(
                    new Book
                    {
                        Id = 1,
                        Title = "Book 1",
                        PublishedDate = new DateTime(2020, 1, 1),
                        AuthorId = 1,
                    },
                    new Book
                    {
                        Id = 2,
                        Title = "Book 2",
                        PublishedDate = new DateTime(2021, 2, 2),
                        AuthorId = 2,
                    },
                    new Book
                    {
                        Id = 3,
                        Title = "Book 3",
                        PublishedDate = new DateTime(2022, 3, 3),
                        AuthorId = 3,
                    }
                );
        }
    }
}
