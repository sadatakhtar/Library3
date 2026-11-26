namespace library_v3.DTOs
{
    public class BookUpdateDTO
    {
        public string? Title { get; set; }
        public DateTime? PublishedDate { get; set; }
        public int AuthorId { get; set; }
    }
}
