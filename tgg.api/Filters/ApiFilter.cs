
using Microsoft.AspNetCore.Mvc.Filters;

namespace TggApi.Filters
{
    public class ApiFilter : IActionFilter
    {
        private readonly ILogger<ApiFilter> _logger;
      

        public ApiFilter(ILogger<ApiFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation("filtro de log");
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation("filtro de log");
        }
    }
}
