using Application.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Restaurant.Contracts.Common;

namespace Restaurant.Filters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var apiError = new ErrorResponse
                {
                    StatusCode = (int)ErrorCode.BadRequest,
                    StatusPhrase = "Bad Request",
                    Timestamp = DateTime.Now
                };

                var errors = context.ModelState.AsEnumerable();

                foreach (var error in errors)
                {
                    foreach (var innerError in error.Value.Errors)
                    {
                        apiError.Errors.Add(innerError.ErrorMessage);
                    }
                }

                context.Result = new BadRequestObjectResult(apiError);
            }
        }
    }
}
