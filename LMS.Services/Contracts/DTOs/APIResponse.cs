namespace LMS.Services.Contracts.DTOs
{
    public sealed class APIResponse
    {
        public string? ResponseCode { get; set; }
        public object? Description { get; set; }
        public object? Param { get; set; }
        public DateTime Time { get; set; }
    }
}
