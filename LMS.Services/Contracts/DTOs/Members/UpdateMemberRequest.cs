
using System.ComponentModel.DataAnnotations;

namespace LMS.Services.Contracts.DTOs.Members
{
    public sealed class UpdateMemberRequest
    {
        [StringLength(100)] public string MemberName { get; set; } = null!;
        [StringLength(6)] public string Gender { get; set; } = null!;
        [StringLength(20)] public string PhoneNo { get; set; } = null!;
        [EmailAddress, StringLength(200)] public string Email { get; set; } = null!;
        [StringLength(300)] public string Address { get; set; } = null!;
    }
}
