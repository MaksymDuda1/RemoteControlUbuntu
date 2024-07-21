using System.Security.Authentication;
using Microsoft.AspNetCore.Mvc;
using RemoteControlUbuntu.Application.Exceptions;

namespace RemoteControlUbuntu.API.Middlewares;

public class ExceptionHandlingMiddleware(
    RequestDelegate next,
    ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            var code = StatusCodes.Status500InternalServerError;

            switch (e)
            {
                case EntityNotFoundException entityNotFoundException:
                    code = StatusCodes.Status404NotFound;
                    break;
                case AuthenticationException authenticationException:
                    code = StatusCodes.Status401Unauthorized;
                    break;
                default:
                    code = StatusCodes.Status500InternalServerError;
                    break;
            }
            
            logger.LogError(e, "Exception occurred: {Message}", e.Message);
            var problemDetails = new ProblemDetails()
            {
                Status = code,
                Title = e.Message,
            };

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }
}