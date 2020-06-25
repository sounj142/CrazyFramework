using Microsoft.AspNetCore.Builder;

namespace CrazyFramework.API.Helpers
{
	public static class ExceptionHandlerMiddlewareExtensions
	{
		public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<ExceptionHandlerMiddleware>();
		}
	}
}