namespace Dummy.Web.Api.Middleware
{
    using Dummy.Web.Api.Models;
    using Microsoft.AspNetCore.Http;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Specialized;
    using System.Net;
    using System.Threading.Tasks;

    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private static readonly log4net.ILog log =
       log4net.LogManager.GetLogger(typeof(Program));

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
                { $"ExceptionHandlingMiddleware caught exception:", ex.GetType().ToString() }
            };

            if (ex is NotImplementedException)
            {
                log.Debug(new NotImplementedException());

                await WriteErrorResponseAsync(context, HttpStatusCode.NotImplemented, null);
            }
            else if (ex is InvalidOperationException)
            {
                log.Debug(new InvalidOperationException());

                await WriteErrorResponseAsync(context, HttpStatusCode.BadRequest, new ErrorDetail(ex.Message));
            }
            else if (ex is ArgumentNullException || ex is ArgumentException)
            {
                log.Debug(new ArgumentException());

                await WriteErrorResponseAsync(context, HttpStatusCode.InternalServerError, new ErrorDetail(ex.Message));
            }
            else if (ex is EntityNotFoundException
                || typeof(EntityNotFoundException<int>).IsAssignableFrom(ex.GetType())
                || typeof(EntityNotFoundException<string>).IsAssignableFrom(ex.GetType()))
            {
                log.Debug(new EntityNotFoundException<int>());

                await WriteErrorResponseAsync(context, HttpStatusCode.NotFound, null);
            }
            else if (typeof(EntityAlreadyExistsException).IsAssignableFrom(ex.GetType()))
            {
                log.Debug(new EntityAlreadyExistsException());

                await WriteErrorResponseAsync(context, HttpStatusCode.BadRequest, new ErrorDetail(ex.Message));
            }
            else if (ex is TimeoutException)
            {
                log.Debug(new TimeoutException());

                await WriteErrorResponseAsync(context, HttpStatusCode.ServiceUnavailable);
            }
            else
            {
                await WriteErrorResponseAsync(context, HttpStatusCode.InternalServerError, new ErrorDetail(ExceptionHandlingMiddleware.DefaultResponseString));
            }
        }

        private async Task WriteErrorResponseAsync(HttpContext context, HttpStatusCode statusCode, object error = null)
        {
            context.Response.Clear();
            context.Response.StatusCode = (int)statusCode;


            if (error != null)
            {
                log.Debug(error);
                await context.Response.WriteAsync(JsonConvert.SerializeObject(error));
            }
            else
            {
                log.Error(error);
            }
        }
    }
}
