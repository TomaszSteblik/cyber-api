using System.Text;
using System.Text.Json;
using Cyber.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using ApplicationException = Cyber.Application.Exceptions.ApplicationException;

namespace Cyber.Infrastructure.Middlewares;

public class ExceptionToHttpMiddleware : IMiddleware
{
    public ExceptionToHttpMiddleware()
    {
        
    }
    
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (DomainException e)
        {
            context.Response.StatusCode = (int) e.StatusCode;
            var messageObject = new
            {
                e.ErrorCode,
                e.Message
            };
            await context.Response.WriteAsync(JsonSerializer.Serialize(messageObject));
        }
        catch (ApplicationException e)
        {
            context.Response.StatusCode = (int) e.StatusCode;
            var messageObject = new
            {
                e.ErrorCode,
                e.Message
            };
            await context.Response.WriteAsync(JsonSerializer.Serialize(messageObject));
        }
        catch (Exception e)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            Console.WriteLine(e);
        }
    }
}