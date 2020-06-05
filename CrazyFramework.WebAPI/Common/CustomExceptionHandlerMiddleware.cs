using CrazyFramework.Core.Common.Exceptions;
using CrazyFramework.Core.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace CrazyFramework.WebAPI.Common
{
	public class CustomExceptionHandlerMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<CustomExceptionHandlerMiddleware> _logger;

		public CustomExceptionHandlerMiddleware(RequestDelegate next, ILogger<CustomExceptionHandlerMiddleware> logger)
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

				context.Response.ContentType = "application/json";
				context.Response.StatusCode = code;
				await context.Response.WriteAsync(content);
			}
		}

		private (int Code, string Content) HandleException(Exception exception)
		{
			HttpStatusCode code;
			string result;

			switch (exception)
			{
				case ValidationException validationException:
					code = HttpStatusCode.BadRequest;
					result = JsonConvert.SerializeObject(validationException.Failures);
					break;

				case NotFoundException notFoundException:
					code = HttpStatusCode.NotFound;
					result = JsonConvert.SerializeObject(DictionaryHelper.CreateErrorObject(notFoundException.ErrorCode, notFoundException.Message));
					break;

				case FrameworkException frameworkException:
					code = HttpStatusCode.BadRequest;
					result = JsonConvert.SerializeObject(DictionaryHelper.CreateErrorObject(frameworkException.ErrorCode, frameworkException.Message));
					break;

				default:
					code = HttpStatusCode.InternalServerError;
					// TODO: apply multi languages
					result = JsonConvert.SerializeObject(DictionaryHelper.CreateErrorObject("Errors", "Unknown error"));
					_logger.LogError(exception, exception.Message);
					break;
			}

			return (Code: (int)code, Content: result);
		}
	}
}