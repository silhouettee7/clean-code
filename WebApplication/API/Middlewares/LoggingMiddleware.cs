using System.Net;

namespace API.Middlewares;

public class LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext httpContext)
    {
        using var streamReader = new StreamReader(httpContext.Request.Body);
        var body = await streamReader.ReadToEndAsync();
        
        var headers = httpContext.Request.Headers
            .Select(x => $"{x.Key}: {string.Join(", ", x.Value)}");
        
        var requestAbout = $"Id: {httpContext.Connection.Id}\n" +
                           $"Method: {httpContext.Request.Method}\n" +
                           $"Url: {httpContext.Request.Path}\n" +
                           $"Headers: {headers}\n" +
                           $"Body: {body}";
        logger.LogInformation(requestAbout);
            
        await next(httpContext);
        
        using var streamWriter = new StreamWriter(httpContext.Response.Body);
        var responseBody = await streamReader.ReadToEndAsync();
        
        var responseAbout = $"Id: {httpContext.Connection.Id}\n" +
                            $"Status code: {httpContext.Response.StatusCode}\n " +
                            $"Body: {responseBody}";
        logger.LogInformation(responseAbout);
    }
}