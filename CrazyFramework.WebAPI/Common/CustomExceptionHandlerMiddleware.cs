using CrazyFramework.Core.Common.Exceptions;
using CrazyFramework.Core.Helpers;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace CrazyFramework.WebAPI.Common
{
	public class CustomExceptionHandlerMiddleware
	{
		private readonly RequestDelegate _next;

		public CustomExceptionHandlerMiddleware(RequestDelegate next)
		{
			_next = next;
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
			var code = HttpStatusCode.InternalServerError;
			var result = string.Empty;

			switch (exception)
			{
				case ValidationException validationException:
					code = HttpStatusCode.BadRequest;
					result = JsonConvert.SerializeObject(validationException.Failures);
					break;

				case NotFoundException _:
					code = HttpStatusCode.NotFound;
					break;

				case FrameworkException frameworkException:
					code = HttpStatusCode.BadRequest;
					result = JsonConvert.SerializeObject(DictionaryHelper.CreateErrorObject(frameworkException.ErrorCode, frameworkException.Message));
					break;

				default:
					// TODO: apply multi languages
					result = JsonConvert.SerializeObject(DictionaryHelper.CreateErrorObject("Errors", "Unknown error"));
					break;
			}

			return (Code: (int)code, Content: result);
		}
	}
}