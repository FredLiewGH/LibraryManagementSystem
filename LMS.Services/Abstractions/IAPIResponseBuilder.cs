using LMS.Services.Contracts.DTOs;

namespace LMS.Services.Abstractions
{
    public interface IAPIResponseBuilder
    {
        APIResponse Success(string? description = null, object? param = null);
        APIResponse Fail(string? description = null, object? param = null);
        APIResponse NotFound(string? description = null, object? param = null);
        APIResponse Conflict(string? description = null, object? param = null);
        APIResponse Error(string? description = null, object? param = null);
    }
}
