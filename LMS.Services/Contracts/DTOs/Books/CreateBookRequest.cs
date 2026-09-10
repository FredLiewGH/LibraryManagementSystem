using System.ComponentModel.DataAnnotations;

namespace LMS.Services.Contracts.DTOs.Books
{
    public sealed class CreateBookRequest
    {
        [Required, StringLength(200)] public string BookName { get; set; } = null!;
        [Required, StringLength(300)] public string Author { get; set; } = null!;
        [Required, StringLength(100)] public string Publisher { get; set; } = null!;
    }
}
