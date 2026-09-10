using LMS.Services.Abstractions;
using LMS.Services.Contracts.DTOs;
using LMS.Services.Contracts.Enums;

namespace LMS.Services.Services
{
    public sealed class APIResponseBuilder : IAPIResponseBuilder
    {
        public APIResponse Success(string? description = null, object? param = null)
        {
            APIResponse msg_complete = new APIResponse();
            msg_complete.ResponseCode = API_RESPONSE_CODE.SUCCESS.ToString();
            msg_complete.Description = string.IsNullOrEmpty(description) ? "" : description;
            msg_complete.Param = param == null ? "" : param;
            msg_complete.Time = DateTime.UtcNow;

            return msg_complete;
        }

        public APIResponse Fail(string? description = null, object? param = null)
        {
            APIResponse msg_complete = new APIResponse();
            msg_complete.ResponseCode = API_RESPONSE_CODE.FAIL.ToString();
            msg_complete.Description = string.IsNullOrEmpty(description) ? "" : description;
            msg_complete.Param = param == null ? "" : param;
            msg_complete.Time = DateTime.UtcNow;

            return msg_complete;
        }
        
        public APIResponse NotFound(string? description = null, object? param = null)
        {
            APIResponse msg_complete = new APIResponse();
            msg_complete.ResponseCode = API_RESPONSE_CODE.NOT_FOUND.ToString();
            msg_complete.Description = string.IsNullOrEmpty(description) ? "No record found." : description;
            msg_complete.Param = param == null ? "" : param;
            msg_complete.Time = DateTime.UtcNow;

            return msg_complete;
        }

        public APIResponse Conflict(string? description = null, object? param = null)
        {
            APIResponse msg_complete = new APIResponse();
            msg_complete.ResponseCode = API_RESPONSE_CODE.CONFLICT.ToString();
            msg_complete.Description = string.IsNullOrEmpty(description) ? "The record was modified by another user." : description;
            msg_complete.Param = param == null ? "" : param;
            msg_complete.Time = DateTime.UtcNow;

            return msg_complete;
        }

        public APIResponse Error(string? description = null, object? param = null)
        {
            APIResponse msg_complete = new APIResponse();
            msg_complete.ResponseCode = API_RESPONSE_CODE.ERROR.ToString();
            msg_complete.Description = string.IsNullOrEmpty(description) ? "Server encountered an error." : description;
            msg_complete.Param = param == null ? "" : param;
            msg_complete.Time = DateTime.UtcNow;

            return msg_complete;
        }
    }
}
