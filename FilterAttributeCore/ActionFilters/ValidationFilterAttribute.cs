using FilterAttributeCore.SaveDb;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FilterAttributeCore.ActionFilters
{
    public class ValidationFilterAttribute : IActionFilter
    {
        private static IApiLogger logger => new ApiLogger();
        public void OnActionExecuting(ActionExecutingContext context)
        {

        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}
