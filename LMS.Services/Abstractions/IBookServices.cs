using LMS.Services.Contracts.DTOs;
using LMS.Services.Contracts.DTOs.Books;

namespace LMS.Services.Abstractions
{
    public interface IBookServices : IBaseSystemProcessor    
    {
        Task<APIResponse> Create(CreateBookRequest request, CancellationToken token = default);
        Task<APIResponse> Update(Guid id, UpdateBookRequest request, CancellationToken token = default);
    }
}
