using LMS.Services.Contracts.DTOs;
using LMS.Services.Contracts.Enums;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Extensions
{
    public static class APIResponseMapperExtensions
    {
        public static IActionResult ToHttpResult(this APIResponse response)
        {
            API_RESPONSE_CODE responseCode = Enum.TryParse(response.ResponseCode, out API_RESPONSE_CODE parsedCode) ? parsedCode : API_RESPONSE_CODE.ERROR;
            var statusCode = responseCode switch
            {
                API_RESPONSE_CODE.SUCCESS => StatusCodes.Status200OK,
                API_RESPONSE_CODE.FAIL => StatusCodes.Status400BadRequest,
                API_RESPONSE_CODE.NOT_FOUND => StatusCodes.Status404NotFound,
                API_RESPONSE_CODE.CONFLICT => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status500InternalServerError
            };
            
            return new ObjectResult(response) { StatusCode = statusCode };
        }
    }
}
