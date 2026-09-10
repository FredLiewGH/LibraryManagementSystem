namespace LMS.Services.Contracts.DTOs.Members
{
    public sealed class MemberResponse
    {
        public Guid MemberId { get; set; }
        public string MemberName { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string PhoneNo { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Address { get; set; } = null!;
    }
}
