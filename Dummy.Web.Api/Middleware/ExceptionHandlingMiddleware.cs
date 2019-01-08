namespace Dummy.Web.Api.Middleware
{
    using System;
    using System.Collections.Specialized;
    using System.Net;
    using System.Threading.Tasks;
    using Dummy.Web.Api.Models;
    using Dummy.Web.Common.Exceptions;
    using Microsoft.AspNetCore.Http;
    using Newtonsoft.Json;

    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(Program));

        private const string DefaultResponseString = "An unexpected error occured. The details of the error have been logged. If this continues to happen please contact your  Administrator.";

        public ExceptionHandlingMiddleware(
            RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var addInfo = new NameValueCollection
            {
                { "ExceptionHandlingMiddleware caught exception:", ex.GetType().ToString() }
            };
            log.Debug(JsonConvert.SerializeObject(addInfo));

            if (ex is NotImplementedException)
            {
                await WriteErrorResponseAsync(context, HttpStatusCode.NotImplemented, null);
            }
            else if (ex is InvalidOperationException)
            {
                await WriteErrorResponseAsync(context, HttpStatusCode.BadRequest, new ErrorDetail(ex.Message));
            }
            else if (ex is InvalidModelException)
            {
                await WriteErrorResponseAsync(context, HttpStatusCode.BadRequest, new ErrorDetail(ex.Message));
            }
            else if (ex is WrongEmailException)
            {
                await WriteErrorResponseAsync(context, HttpStatusCode.BadRequest, new ErrorDetail(ex.Message));
            }
            else if (ex is EntityNotFoundException
                || typeof(EntityNotFoundException<int>).IsAssignableFrom(ex.GetType())
                || typeof(EntityNotFoundException<string>).IsAssignableFrom(ex.GetType()))
            {
                await WriteErrorResponseAsync(context, HttpStatusCode.NotFound, addInfo);
            }
            else if (typeof(EntityAlreadyExistsException).IsAssignableFrom(ex.GetType()))
            {
                await WriteErrorResponseAsync(context, HttpStatusCode.BadRequest, new ErrorDetail(ex.Message));
            }
            else if (ex is TimeoutException)
            {
                await WriteErrorResponseAsync(context, HttpStatusCode.ServiceUnavailable);
            }
            else
            {
                await WriteErrorResponseAsync(context, HttpStatusCode.InternalServerError, new ErrorDetail(DefaultResponseString));
            }
        }

        private async Task WriteErrorResponseAsync(HttpContext context, HttpStatusCode statusCode, object error = null)
        {
            context.Response.Clear();
            context.Response.StatusCode = (int)statusCode;

            if (error != null)
            {
                log.Debug(JsonConvert.SerializeObject(error));
                await context.Response.WriteAsync(JsonConvert.SerializeObject(error));
            }
            else
            {
                log.Error(DefaultResponseString);
            }
        }
    }
}
