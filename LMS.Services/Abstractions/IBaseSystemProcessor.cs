using LMS.Services.Contracts.DTOs;

namespace LMS.Services.Abstractions
{
    public interface IBaseSystemProcessor
    {
        Task<APIResponse> GetAll(CancellationToken token = default);
        Task<APIResponse> GetById(Guid id, CancellationToken token = default);        
        Task<APIResponse> Delete(Guid id, CancellationToken token = default);
    }
}
