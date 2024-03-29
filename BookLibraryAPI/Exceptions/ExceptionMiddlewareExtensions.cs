using BookLibraryAPI.ViewModels;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Features;
using System.Net;

namespace BookLibraryAPI.Exceptions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureBuiltInExceptionHandler(this IApplicationBuilder applicationBuilder, ILoggerFactory loggerFactory)
        {
            applicationBuilder.UseExceptionHandler(app =>
            {
                app.Run(async context =>
                {
                    var logger = loggerFactory.CreateLogger("ConfigureBuiltInExceptionHandler");

                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var contextRequest = context.Features.Get<IHttpRequestFeature>();

                    if (contextFeature != null)
                    {
                        var errorVMString = new ErrorVM()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = contextFeature.Error.Message,
                            Path = contextRequest != null ? contextRequest.Path : "path unknown"
                        }.ToString();

                        logger.LogError(errorVMString);

                        await context.Response.WriteAsync(errorVMString);
                    }
                });
            });
        }

        public static void ConfigureCustomExceptionHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomExceptionMiddleware>();
        }
    }
}