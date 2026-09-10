using System.ComponentModel.DataAnnotations;

namespace LMS.Services.Contracts.DTOs
{
    public sealed class Borrows
    {        
        [Required] public Guid BookId { get; set; }
        [Required] public Guid MemberId { get; set; }
        [Required] public DateOnly BorrowStartDate { get; set; }
        [Required] public DateOnly BorrowEndDate { get; set; }
    }
}
