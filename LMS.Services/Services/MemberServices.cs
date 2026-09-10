using LMS.EFCore.Abstractions;
using LMS.EFCore.Entities;
using LMS.Services.Abstractions;
using LMS.Services.Contracts.Constants;
using LMS.Services.Contracts.DTOs;
using LMS.Services.Contracts.DTOs.Members;
using Microsoft.Extensions.Logging;

namespace LMS.Services.Services
{
    public sealed class MemberServices : ServiceExecution, IMemberServices
    {
        private readonly ICRUD<Tblmembers> _crudMembers;
        private readonly ICRUD<Tblborrows> _crudBorrows;
        private readonly IDBCommit _commiter;

        public MemberServices(
            ICRUD<Tblmembers> crudMembers,
            ICRUD<Tblborrows> crudBorrows,
            IDBCommit commiter,
            IAPIResponseBuilder apiResBuilder, ILogger<MemberServices> logger) : base(apiResBuilder, logger)

        {
            _crudMembers = crudMembers ?? throw new ArgumentNullException(nameof(crudMembers));
            _crudBorrows = crudBorrows ?? throw new ArgumentNullException(nameof(crudBorrows));
            _commiter = commiter ?? throw new ArgumentNullException(nameof(commiter));
        }

        public Task<APIResponse> GetAll(CancellationToken token = default) => Execute(async () =>
        {
            IReadOnlyList<Tblmembers> members = await _crudMembers.GetAllAsync(token);

            List<MemberResponse> result = members.Select(b => new MemberResponse
            {
                MemberId = b.MemberID,
                MemberName = b.MemberName,
                Gender = b.Gender,
                PhoneNo = b.PhoneNo,
                Email = b.Email,
                Address = b.Address
            }).ToList();

            return _apiResBuilder.Success(null, result);
        });

        public Task<APIResponse> GetById(Guid id, CancellationToken token = default) => Execute(async () =>
        {
            ArgumentNullException.ThrowIfNull(id, nameof(id));
            Tblmembers? existing_member = await _crudMembers.GetByIdAsync(id, token);
            if (existing_member == null)
            {
                return _apiResBuilder.NotFound();
            }

            MemberResponse result = new MemberResponse
            {
                MemberId = existing_member.MemberID,
                MemberName = existing_member.MemberName,
                Gender = existing_member.Gender,
                PhoneNo = existing_member.PhoneNo,
                Email = existing_member.Email,
                Address = existing_member.Address
            };

            return _apiResBuilder.Success(null, result);
        });

        public Task<APIResponse> Create(CreateMemberRequest request, CancellationToken token = default) => Execute(async () =>
        {
            ArgumentNullException.ThrowIfNull(request, nameof(request));            
            // Check if record exists
            IReadOnlyList<Tblmembers>? existing_member = await _crudMembers.FindAsync(x => x.PhoneNo == request.PhoneNo || x.Email == request.Email, token);
            if (existing_member != null && existing_member.Count > 0)
            {
                return _apiResBuilder.Conflict("Member already exists!");
            }

            Tblmembers newMember = new Tblmembers
            {
                MemberID = Guid.NewGuid(),
                MemberName = request.MemberName,
                Gender = request.Gender,
                PhoneNo = request.PhoneNo,
                Email = request.Email,
                Address = request.Address,
                CreatedTimestamp = DateTime.UtcNow,
            };

            await _crudMembers.AddAsync(newMember, token);
            await _commiter.SaveChangesAsync(token);

            return _apiResBuilder.Success(Messages.CreateSuccessMessage);
        });

        public Task<APIResponse> Update(Guid id, UpdateMemberRequest request, CancellationToken token = default) => Execute(async () =>
        {
            ArgumentNullException.ThrowIfNull(request, nameof(request));
            // Check if record exists
            Tblmembers? existing_member = await _crudMembers.GetByIdAsync(id, token);
            if (existing_member == null)
            {
                return _apiResBuilder.NotFound();
            }

            existing_member.MemberName = request.MemberName ?? existing_member.MemberName;
            existing_member.Gender = request.Gender ?? existing_member.Gender;
            existing_member.PhoneNo = request.PhoneNo ?? existing_member.PhoneNo;
            existing_member.Email = request.Email ?? existing_member.Email;
            existing_member.Address = request.Address ?? existing_member.Address;

            await _crudMembers.UpdateAsync(existing_member, token);
            await _commiter.SaveChangesAsync(token);

            return _apiResBuilder.Success(Messages.UpdateSuccessMessage);
        });

        public Task<APIResponse> Delete(Guid id, CancellationToken token = default) => Execute(async () =>
        {
            // Check if record exists
            Tblmembers? existing_member = await _crudMembers.GetByIdAsync(id, token);
            if (existing_member == null)
            {
                return _apiResBuilder.NotFound();
            }

            // Check if member currently borrowed book
            IReadOnlyList<Tblborrows> existing_borrows = await _crudBorrows.FindAsync(x => x.MemberID == existing_member.MemberID, token);
            if (existing_borrows != null && existing_borrows.Count > 0)
            {
                return _apiResBuilder.Conflict("Cannot delete a member that is currently borrowed book.");
            }

            await _crudMembers.DeleteAsync(existing_member, token);
            await _commiter.SaveChangesAsync(token);

            return _apiResBuilder.Success(Messages.DeleteSuccessMessage);
        });
    }
}
