using LMS.Services.Contracts.DTOs;
using LMS.Services.Contracts.DTOs.Members;

namespace LMS.Services.Abstractions
{
    public interface IMemberServices : IBaseSystemProcessor
    {
        Task<APIResponse> Create(CreateMemberRequest request, CancellationToken token = default);
        Task<APIResponse> Update(Guid id, UpdateMemberRequest request, CancellationToken token = default);
    }
}
