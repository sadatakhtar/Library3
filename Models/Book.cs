namespace library_v3.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public DateTime? PublishedDate { get; set; }
        //FK
        public int AuthorId { get; set; }
        //Navigation property
        public Author? Author { get; set; }
    }
}
