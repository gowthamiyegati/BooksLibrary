using BookLibraryAPI.ViewModels;
using Microsoft.AspNetCore.Http.Features;
using System.Net;

namespace BookLibraryAPI.Exceptions
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(httpContext, e);
            }
        }

        private static Task HandleExceptionAsync(HttpContext httpContext, Exception e)
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            httpContext.Response.ContentType = "application/json";
            var contextRequest = httpContext.Features.Get<IHttpRequestFeature>();

            var response = new ErrorVM()
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = $"Internal server error form custom md-ware. {e.Message}",
                Path = contextRequest != null ? contextRequest.Path : "path unknown",
            };

            return httpContext.Response.WriteAsync(response.ToString());
        }
    }
}