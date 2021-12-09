using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace FilterAttributeCore.ActionFilters
{
    // this filter is applied globally during configuration of web application pipeline
    public class ExcutionTimeFilterAttribute : Attribute,IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            // check whether SuppressLoggingAttribute was applied to current request
            // we check it here in the end of the request because we don't want to depend
            // on the order in which filters are configured in the pipeline
            if (!context.HttpContext.Items.ContainsKey(typeof(SuppressItemKey)))
            {
                // since SuppressItemKey was not set for the current request, 
                // we can do the logging stuff
                var clock = (Stopwatch)context.HttpContext.Items[typeof(StopwatchItemKey)];
                var elapsedMilliseconds = clock.ElapsedMilliseconds;
                //context.Result = new ObjectResult(TimeSpan.FromMilliseconds(elapsedMilliseconds).TotalSeconds);
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            // here we save timestamp at the beginning of the request
            // I use Stopwatch because it's handy in this case
            context.HttpContext.Items[typeof(StopwatchItemKey)] = Stopwatch.StartNew();
        }

        // we use private class types as keys for HttpContext.Items dictionary
        // this is better than using strings as the keys, because 
        // it avoids accidental collisions with other code that uses HttpContext.Items
        private class StopwatchItemKey { }
        private class SuppressItemKey { }
    }
}
