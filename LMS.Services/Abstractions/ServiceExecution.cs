using LMS.Services.Contracts.Constants;
using LMS.Services.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;

namespace LMS.Services.Abstractions
{
    public abstract class ServiceExecution
    {
        protected readonly IAPIResponseBuilder _apiResBuilder;
        private readonly ILogger _logger;

        protected ServiceExecution(IAPIResponseBuilder apiResBuilder, ILogger logger)
        {
            _apiResBuilder = apiResBuilder ?? throw new ArgumentNullException(nameof(apiResBuilder));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected async Task<APIResponse> Execute(
            Func<Task<APIResponse>> action,
            [CallerMemberName] string functionName = "")
        {
            try
            {
                return await action();
            }
            catch (Exception ex)
            { 
                Console.WriteLine("[{0}.{1}] {2}{3}", GetType().Name, functionName, Messages.ErrorMessagePrefix, ex.Message);
                _logger.LogError(ex, "[{ServiceName}.{FunctionName}] {ErrorMessagePrefix}{ErrorMessage}", GetType().Name, functionName, Messages.ErrorMessagePrefix, ex.Message);
                return _apiResBuilder.Error();
            }
        }
    }
}
