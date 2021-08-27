using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using BlockingService.Exceptions;
using System;

namespace BlockingService.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        [Route("errors")]
        public void Error()
        {
            IExceptionHandlerFeature context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            Exception ex = context?.Error;

            HttpContext.Response.ContentType = "application/json";

            int statusCode = 500;
            string message = "Error occurred while processing your request.";

            if (ex is BusinessException businessException)
            {
                statusCode = businessException.StatusCode;
                message = businessException.Message;
            }
            else if (ex != null)
            {
                statusCode = Response.StatusCode;
                message = ex.Message;
            }

            HttpContext.Response.StatusCode = statusCode;
            HttpContext.Response.WriteAsync(JsonConvert.SerializeObject(new { message, statusCode }));
        }

        [Route("errors-development")]
        public void ErrorDevelopment()
        {
            IExceptionHandlerPathFeature context = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            Exception ex = context?.Error;

            HttpContext.Response.ContentType = "application/json";

            int statusCode = Response.StatusCode;
            string message = "Error occurred while processing your request.";

            if (ex is BusinessException businessException)
            {
                statusCode = businessException.StatusCode;
                message = businessException.Message;
            }

            HttpContext.Response.StatusCode = statusCode;

            if (ex != null)
            {
                HttpContext.Response.WriteAsync(JsonConvert.SerializeObject(new
                {
                    context.Path,
                    ex.Message,
                    StatusCode = statusCode,
                    ex.StackTrace,
                    ex.InnerException
                }));
            }
            else
            {
                HttpContext.Response.WriteAsync(JsonConvert.SerializeObject(new { message, statusCode }));
            }
        }
    }
}
