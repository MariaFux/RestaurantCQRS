using Application.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Restaurant.Contracts.Common;

namespace Restaurant.Filters
{
    public class ExceptionHandler : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var apiError = new ErrorResponse
            {
                StatusCode = (int)ErrorCode.ServerError,
                StatusPhrase = "Internal Server Error",
                Timestamp = DateTime.Now
            };
            apiError.Errors.Add(context.Exception.Message);

            context.Result = new JsonResult(apiError) { StatusCode = (int)ErrorCode.ServerError };
        }
    }
}
