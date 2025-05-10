using Application.ModelCaptureException;
using Newtonsoft.Json;
using System.Net;

namespace WebApi.Middleware
{
    public class ExecuteExceptionCapture
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExecuteExceptionCapture> _logger;  
        public ExecuteExceptionCapture(RequestDelegate next, 
            ILogger<ExecuteExceptionCapture> logger)
        {
            _next = next;
           _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception exeotionCapture)
            {
                await EvaluateCapturedExceptionAsync(httpContext,exeotionCapture,_logger);
            }
        }

        private async Task EvaluateCapturedExceptionAsync(HttpContext context, Exception exception,
            ILogger<ExecuteExceptionCapture> logger)
        {
            object errors = null;
            switch (exception)
            {
                case CaptureExceptions capture:
                    logger.LogError(exception, "Manejador de Error!.");
                    errors = capture.errorsCapure;
                    context.Response.StatusCode = (int)capture.statusCodeCapture;
                    break;
                case Exception ex:
                    logger.LogError(ex, "Error de Servidor.");
                    errors = string.IsNullOrWhiteSpace(ex.Message) ? "Error": ex.Message;
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            context.Response.ContentType = "application/json";
            if (errors != null) 
            { 
              var result = JsonConvert.SerializeObject(new { errors });
                await context.Response.WriteAsync(result);
            }
        }
    }
}
