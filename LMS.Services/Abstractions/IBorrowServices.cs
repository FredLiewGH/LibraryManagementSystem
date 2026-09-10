using LMS.Services.Contracts.DTOs;

namespace LMS.Services.Abstractions
{
    public interface IBorrowServices
    {
        Task<APIResponse> BorrowBook(Borrows borrow, CancellationToken token = default);
        Task<APIResponse> ReturnBook(Guid id, CancellationToken token = default);
    }
}
