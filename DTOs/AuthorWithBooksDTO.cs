namespace library_v3.DTOs
{
    public class AuthorWithBooksDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Country { get; set; }
        public List<BookDTO> Books { get; set; } = new();
    }
}
