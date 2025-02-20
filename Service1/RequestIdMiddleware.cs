using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Serilog;

public class RequestIdMiddleware
{
    private readonly RequestDelegate _next;

    public RequestIdMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        // Проверяем, есть ли заголовок X-Request-ID
        if (!context.Request.Headers.ContainsKey("X-Request-ID"))
        {
            // Генерируем новый идентификатор, если заголовок отсутствует
            var requestId = Guid.NewGuid().ToString();
            context.Request.Headers["X-Request-ID"] = requestId;

            // Логируем новый идентификатор
            Log.Information("Generated new Request ID: {RequestId}", requestId);
        }

        // Добавляем Request ID в заголовки ответа
        context.Response.OnStarting(() =>
        {
            context.Response.Headers["X-Request-ID"] = context.Request.Headers["X-Request-ID"];
            return Task.CompletedTask;
        });

        // Передаем управление следующему middleware
        await _next(context);
    }
}
