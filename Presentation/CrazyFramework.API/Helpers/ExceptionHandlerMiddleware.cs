using CrazyFramework.App.Common.Exceptions;
using CrazyFramework.App.Entities;
using CrazyFramework.App.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace CrazyFramework.API.Helpers
{
	public class ExceptionHandlerMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<ExceptionHandlerMiddleware> _logger;
		private readonly IWebHostEnvironment _environment;
		private readonly AppSettings _appSettings;

		public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger, IWebHostEnvironment environment, AppSettings appSettings)
		{
			_next = next;
			_logger = logger;
			_environment = environment;
			_appSettings = appSettings;
		}

		public async Task Invoke(HttpContext context)
		{
			if (_appSettings.UseCustomExceptionHandler)
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
			else
			{
				await _next(context);
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

					// TODO: apply multi languages
					var message = _environment.IsDevelopment() ? exception.Message : "Unknown error";

					return (
						Code: (int)HttpStatusCode.InternalServerError,
						Content: JsonConvert.SerializeObject(DictionaryHelper.CreateErrorObject("Errors", message))
					);
				}))()
			};
	}
}