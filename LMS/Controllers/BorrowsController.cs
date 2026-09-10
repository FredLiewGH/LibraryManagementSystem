using LMS.Extensions;
using LMS.Services.Abstractions;
using LMS.Services.Contracts.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BorrowsController : ControllerBase
    {
        private readonly IBorrowServices _borrowService;

        public BorrowsController(IBorrowServices borrowService) 
        { 
            _borrowService = borrowService ?? throw new ArgumentNullException(nameof(borrowService));
        }

        [HttpPost]
        [Route("borrow")]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Borrow(Borrows borrow, CancellationToken token = default)
        {
            var result = await _borrowService.BorrowBook(borrow, token);
            return result.ToHttpResult();
        }

        [HttpPut]
        [Route("return/{bookId}")]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Return(Guid bookId, CancellationToken token = default)
        {
            var result = await _borrowService.ReturnBook(bookId, token);
            return result.ToHttpResult();
        }
    }
}
