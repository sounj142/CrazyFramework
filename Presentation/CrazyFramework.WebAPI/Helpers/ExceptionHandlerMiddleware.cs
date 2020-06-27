using CrazyFramework.App.Common.Exceptions;
using CrazyFramework.App.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace CrazyFramework.WebAPI.Helpers
{
	public class ExceptionHandlerMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<ExceptionHandlerMiddleware> _logger;

		public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
		{
			_next = next;
			_logger = logger;
		}

		public async Task Invoke(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (Exception ex)
			{
				var (code, content) = HandleException(ex);

				context.Response.ContentType = "application/json; charset=utf-8";
				context.Response.StatusCode = code;
				await context.Response.WriteAsync(content);
			}
		}

		private (int Code, string Content) HandleException(Exception exception)
			=> exception switch
			{
				ValidationException validationException => (
					Code: (int)HttpStatusCode.BadRequest,
					Content: JsonConvert.SerializeObject(validationException.Failures)
				),
				NotFoundException notFoundException => (
					Code: (int)HttpStatusCode.NotFound,
					Content: JsonConvert.SerializeObject(DictionaryHelper.CreateErrorObject(notFoundException.ErrorCode, notFoundException.Message))
				),
				FrameworkException frameworkException => (
					Code: (int)HttpStatusCode.BadRequest,
					Content: JsonConvert.SerializeObject(DictionaryHelper.CreateErrorObject(frameworkException.ErrorCode, frameworkException.Message))
				),
				_ => ((Func<(int Code, string Content)>)(() =>
				{
					// we expect that before throw FrameworkException and its derived exceptions, developers should write necessary logs
					// so we only need to write log for other kinds of unhandled exception
					_logger.LogError(exception, exception.Message);

					return (
						Code: (int)HttpStatusCode.InternalServerError,
						// TODO: apply multi languages
						Content: JsonConvert.SerializeObject(DictionaryHelper.CreateErrorObject("Errors", "Unknown error"))
					);
				}))()
			};
	}
}