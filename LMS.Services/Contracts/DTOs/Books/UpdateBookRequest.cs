using System.ComponentModel.DataAnnotations;

namespace LMS.Services.Contracts.DTOs.Books
{
    public sealed class UpdateBookRequest
    {
        [StringLength(200)] public string? BookName { get; set; }
        [StringLength(300)] public string? Author { get; set; }
        [StringLength(100)] public string? Publisher { get; set; }
    }
}
