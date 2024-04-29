using System.Net;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace MovieAPI.Middleware
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                // process exception
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // use log to save

            //  status code response
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            // reponse json type and alert error
            context.Response.ContentType = "application/json";
            var errorMessage = JsonConvert.SerializeObject(new { error = exception.Message });
            await context.Response.WriteAsync(errorMessage);
        }
    }
}