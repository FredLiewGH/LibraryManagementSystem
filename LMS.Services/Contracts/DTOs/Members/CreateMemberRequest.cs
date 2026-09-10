using System.ComponentModel.DataAnnotations;

namespace LMS.Services.Contracts.DTOs.Members
{
    public sealed class CreateMemberRequest
    {
        [Required, StringLength(100)] public string MemberName { get; set; } = null!;
        [Required, StringLength(6)] public string Gender { get; set; } = null!;
        [Required, StringLength(20)] public string PhoneNo { get; set; } = null!;
        [Required, EmailAddress, StringLength(200)] public string Email { get; set; } = null!;
        [StringLength(300)] public string Address { get; set; } = null!;
    }
}
