using System.Diagnostics;

namespace dotnet_webapi.Shared;

public class LogMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LogMiddleware> _logger;

    public LogMiddleware(RequestDelegate next, ILogger<LogMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext, RequestContext requestContext)
    {
        var stopwatch = Stopwatch.StartNew();

        requestContext.Metodo = httpContext.Request.Method;
        requestContext.Url = httpContext.Request.Path;

        requestContext.Usuario = httpContext.User.Identity?.Name ?? "Anonimo";

        foreach (var header in httpContext.Request.Headers)
        {
            if (header.Key != "Authorization")
            {
                requestContext.Headers.TryAdd(header.Key, header.Value.ToString());
            }
        }

        try
        {
            await _next(httpContext);

            requestContext.StatusCode = httpContext.Response.StatusCode;

            if (requestContext.StatusCode >= 500)
                _logger.LogError("Erro Servidor: {Log}", requestContext.ToString());
            else if (requestContext.StatusCode >= 400)
                _logger.LogWarning("Requisição Inválida: {Log}", requestContext.ToString());
            else
                _logger.LogInformation("Sucesso: {Log}", requestContext.ToString());
        }
        catch (Exception ex)
        {
            requestContext.StatusCode = 500;
            requestContext.ErroMensagem = ex.Message;

            _logger.LogError(ex, "Falha Crítica na Requisição: {Log}", requestContext.ToString());

            throw;
        }
        finally
        {
            stopwatch.Stop();
            requestContext.TempoProcessamentoMs = stopwatch.ElapsedMilliseconds;
        }
    }
}
