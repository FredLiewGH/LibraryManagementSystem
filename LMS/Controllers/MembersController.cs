using LMS.Extensions;
using LMS.Services.Abstractions;
using LMS.Services.Contracts.DTOs;
using LMS.Services.Contracts.DTOs.Members;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MembersController : ControllerBase
    {
        private readonly IMemberServices _memberService;

        public MembersController(IMemberServices memberService) 
        {
            _memberService = memberService ?? throw new ArgumentNullException(nameof(memberService));
        }

        [HttpGet]
        [Route("getall")]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll(CancellationToken token = default)
        {
            var result = await _memberService.GetAll(token);
            return result.ToHttpResult();
        }

        [HttpGet]
        [Route("getbyid/{id}")]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken token = default)
        {
            var result = await _memberService.GetById(id, token);
            return result.ToHttpResult();
        }

        [HttpPost]
        [Route("addnew")]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddNew(CreateMemberRequest request, CancellationToken token = default)
        {
            var result = await _memberService.Create(request, token);
            return result.ToHttpResult();
        }

        [HttpPut]
        [Route("modify/{id}")]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Modify(Guid id, UpdateMemberRequest request, CancellationToken token = default)
        {
            var result = await _memberService.Update(id, request, token);
            return result.ToHttpResult();
        }

        [HttpDelete]
        [Route("remove/{id}")]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Remove(Guid id, CancellationToken token = default)
        {
            var result = await _memberService.Delete(id, token);
            return result.ToHttpResult();
        }
    }
}
