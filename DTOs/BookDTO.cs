namespace library_v3.DTOs
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public DateTime? PublishedDate { get; set; }
        public int AuthorId { get; set; }

    }
}
