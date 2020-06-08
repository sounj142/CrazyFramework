using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace CrazyFramework.SpaApp.Angular.Helpers
{
	public class TokenEndpointMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly string _tokenUrl;

		public TokenEndpointMiddleware(RequestDelegate next, string tokenUrl)
		{
			_next = next;
			_tokenUrl = tokenUrl;
		}

		public async Task Invoke(HttpContext context)
		{
			if (context.Request.Path.HasValue && context.Request.Path.Value.StartsWith(_tokenUrl, StringComparison.OrdinalIgnoreCase))
			{
				var accessToken = await context.GetTokenAsync("access_token");

				context.Response.ContentType = "application/json; charset=utf-8";
				context.Response.StatusCode = (int)HttpStatusCode.OK;
				var content = JsonConvert.SerializeObject(new { AccessToken = accessToken });
				await context.Response.WriteAsync(content);
			}
			else
			{
				await _next(context);
			}
		}
	}

	public static class TokenGetterMiddlewareExtensions
	{
		public static IApplicationBuilder UseTokenEndpoint(this IApplicationBuilder builder, string tokenUrl)
		{
			return builder.UseMiddleware<TokenEndpointMiddleware>(tokenUrl);
		}
	}
}