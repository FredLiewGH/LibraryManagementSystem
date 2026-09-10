using LMS.Extensions;
using LMS.Services.Abstractions;
using LMS.Services.Contracts.DTOs;
using LMS.Services.Contracts.DTOs.Books;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookServices _bookService;

        public BooksController(IBookServices bookService) 
        { 
            _bookService = bookService ?? throw new ArgumentNullException(nameof(bookService));
        }

        [HttpGet]
        [Route("getall")]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll(CancellationToken token = default)
        {
            var result = await _bookService.GetAll(token);
            return result.ToHttpResult();
        }

        [HttpGet]
        [Route("getbyid/{id}")]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken token = default)
        {
            var result = await _bookService.GetById(id, token);
            return result.ToHttpResult();
        }

        [HttpPost]
        [Route("addnew")]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddNew(CreateBookRequest request, CancellationToken token = default)
        {
            var result = await _bookService.Create(request, token);
            return result.ToHttpResult();
        }

        [HttpPut]
        [Route("modify/{id}")]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Modify(Guid id, UpdateBookRequest request, CancellationToken token = default)
        {
            var result = await _bookService.Update(id, request, token);
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
            var result = await _bookService.Delete(id, token);
            return result.ToHttpResult();
        }
    }
}
