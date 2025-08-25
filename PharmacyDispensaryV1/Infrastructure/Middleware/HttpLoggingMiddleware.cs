using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.WebUtilities;
using Serilog;
using Serilog.Context;
using System.Diagnostics;
using System.Text;

namespace PharmacyDispensaryV1.Infrastructure.Middleware
{
    public class HttpLoggingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            string correlationId = context.TraceIdentifier.Replace(":", string.Empty);

            using (LogContext.PushProperty("CorrelationId", correlationId))
            {
                var stopwatch = Stopwatch.StartNew();
                await LogRequest(context);
                await CaptureAndLogResponse(context, next, stopwatch);
            }
        }

        private static async Task LogRequest(HttpContext context)
        {
            var requestBody = await ReadRequestBody(context.Request);

            Log.Information(
                "Type=Request Method={Method} Ip={Ip} Url={Url} ContentType={ContentType} RequestBody={RequestBody}",
                context.Request.Method,
                context.Connection.RemoteIpAddress,
                context.Request.GetDisplayUrl(),
                context.Request.ContentType,
                requestBody
            );
        }

        private static async Task CaptureAndLogResponse(HttpContext context, RequestDelegate next, Stopwatch stopwatch)
        {
            var originalBodyStream = context.Response.Body;
            await using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            await next(context);

            stopwatch.Stop();
            await LogResponse(context, stopwatch.ElapsedMilliseconds);

            await responseBody.CopyToAsync(originalBodyStream);
        }

        private static async Task LogResponse(HttpContext context, long duration)
        {
            var statusCode = context.Response.StatusCode;
            var responseBody = await ReadResponseBody(context.Response);

            Log.Information(
                "Type=Response Status={Status} StatusCode={StatusCode} Time={Time}ms ResponseBody={ResponseBody}",
                ReasonPhrases.GetReasonPhrase(statusCode),
                statusCode,
                duration,
                responseBody
            );
        }

        private static async Task<string> ReadRequestBody(HttpRequest request)
        {
            if (request.ContentLength is null or 0)
            {
                return "No body";
            }

            if (request.ContentType != null && request.ContentType.Contains("multipart/form-data", StringComparison.OrdinalIgnoreCase))
            {
                var form = await request.ReadFormAsync();
                var fileDetails = new StringBuilder();
                foreach (var file in form.Files)
                {
                    fileDetails.Append($"FileName: {file.FileName}, ContentType: {file.ContentType}, Size: {file.Length} bytes; ");
                }
                return $"Multipart/FormData detected. Files: [{fileDetails.ToString().TrimEnd(' ', ';')}]";
            }

            request.EnableBuffering();
            var body = await new StreamReader(request.Body, Encoding.UTF8).ReadToEndAsync();
            var cleanBody = body.Replace("\n", string.Empty).Replace(" ", string.Empty);
            request.Body.Position = 0;

            return cleanBody;
        }

        private static async Task<string> ReadResponseBody(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            string text = await new StreamReader(response.Body, Encoding.UTF8).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);
            return text;
        }
    }
}