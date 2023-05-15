using System.Diagnostics;

namespace CSN.WebApi.Middlewares;


public class TimingMiddleware
{
    private readonly RequestDelegate next;
    private readonly ILogger<TimingMiddleware> logger;
    public TimingMiddleware(RequestDelegate next, ILogger<TimingMiddleware> logger)
    {
        this.next = next;
        this.logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();

        await this.next(context);

        stopwatch.Stop();
        var elapsed = stopwatch.Elapsed;
        var controllerName = context.Request.RouteValues["controller"] ?? "??";
        var actionName = context.Request.RouteValues["action"] ?? "??";
        this.logger.LogInformation($"[TimingMiddleware] {controllerName}.{actionName} {elapsed} ms");
    }
}