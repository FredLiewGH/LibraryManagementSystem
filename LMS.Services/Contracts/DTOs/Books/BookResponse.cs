namespace LMS.Services.Contracts.DTOs.Books
{
    public sealed class BookResponse
    {
        public Guid BookId { get; set; }
        public string BookName { get; set; } = null!;
        public string Author { get; set; } = null!;
        public string Publisher { get; set; } = null!;
        public string Status { get; set; } = null!;
    }
}
