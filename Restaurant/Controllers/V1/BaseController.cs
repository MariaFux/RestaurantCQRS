using Application.Contracts.Common;
using Application.Enums;
using Application.Models;
using Azure;
using Microsoft.AspNetCore.Mvc;

namespace Restaurant.Controllers.V1
{
    public class BaseController : ControllerBase
    {
        protected IActionResult HandleErrorResponse(List<Error> errors)
        {
            var apiError = new ErrorResponse();

            if (errors.Any(e => e.Code == ErrorCode.NotFound))
            {
                var error = errors.First(e => e.Code == ErrorCode.NotFound);
                
                apiError.StatusCode = (int)ErrorCode.NotFound;
                apiError.StatusPhrase = "Not Found";
                apiError.Timestamp = DateTime.Now;
                apiError.Errors.Add(error.Message);

                return NotFound(apiError);
            }

            apiError.StatusCode = (int)ErrorCode.ServerError;
            apiError.StatusPhrase = "Internal server error";
            apiError.Timestamp = DateTime.Now;
            apiError.Errors.Add("Unknown error");

            return StatusCode((int)ErrorCode.ServerError, apiError);
        }
    }
}
