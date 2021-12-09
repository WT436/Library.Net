using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using FilterAttributeCore.SaveDb;

namespace FilterAttributeCore.ExceptionFilter
{
    public class ProcessExceptionFilterAttribute : IExceptionFilter
    {

        private static IApiLogger logger => new ApiLogger();

        public void OnException(ExceptionContext context)
        {
            HttpStatusCode status = HttpStatusCode.InternalServerError;
            var message = "Server error occurred.";
            var exceptionType = context.Exception.GetType();
            message = ProcessException(context.Exception);
            context.ExceptionHandled = true;
            HttpResponse response = context.HttpContext.Response;
            response.StatusCode = (int)status;
            response.ContentType = "application/json";
            context.Result = new ObjectResult("Server error occurred.");
        }

        private string ProcessException(Exception exception)
        {
            if (exception == null) return string.Empty;
            logger.LogError(exception);
            if (exception is AggregateException)
                return ProcessAggregateException(exception);
            if (exception is CustomException)
                return exception.Message;
            return exception.Message;
        }

        private string ProcessAggregateException(Exception exception)
        {
            var aggregateException = exception as AggregateException;
            var exceptions = aggregateException.Flatten();
            var errorMessages = new List<string>();
            foreach (var innerException in exceptions.InnerExceptions)
            {
                var errorMessage = ProcessException(exception);
                if (!string.IsNullOrWhiteSpace(errorMessage))
                    errorMessages.Add(errorMessage);
            }
            return errorMessages.Any() ? string.Join("\n", errorMessages) : string.Empty;
        }
    }
}
