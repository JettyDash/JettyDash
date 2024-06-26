using System.Text.Json;
using Schemes.Exception;

namespace Api.Middleware;

public class GlobalExceptionHandlerMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next ?? throw new ArgumentNullException(nameof(next));

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (HttpException ex)
        {
            await HandleExceptionAsync(context, ex, ex.StatusCode);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex, StatusCodes.Status500InternalServerError);
        }

    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception, int statusCode)
    {
        context.Response.ContentType = Constants.ContentType.Json;
        context.Response.StatusCode = statusCode;

        return context.Response.WriteAsync(new ErrorDetails
        {
            StatusCode = statusCode,
            Message = exception.Message
        }.ToString());
    }
}

public class ErrorDetails
{
    public int StatusCode { get; set; }
    public string Message { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}